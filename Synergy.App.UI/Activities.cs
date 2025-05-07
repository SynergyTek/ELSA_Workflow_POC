using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime.Stimuli;
using Elsa.Workflows.Signals;
using Synergy.App.Business.Interface;
using WorkflowStatus = Synergy.App.Data.WorkflowStatus;

namespace Synergy.App.UI;

public record CreateTaskStimulus(Guid TaskId);

[FlowNode("Approved", "Rejected", "Cancelled")]
[Activity("Synergy", "Assign task to user")]
public class AssignTaskToUser : Activity
{
    /// <summary>A list of expected outcomes to handle.</summary>
    [Input(Description = "A list of expected outcomes to handle.", UIHint = "dynamic-outcomes", DefaultValue="Approved, Rejected, Cancelled" )]
    public Input<IList<string>> Branches { get; set; } = null!;

    [Input(Description = "Assign task to user")]
    public Input<string> Title { get; set; } = null!;

    public Input<Guid> AssignToUserId { get; set; } = null!;

    [Output(Description = "Task Id")] public Output<string> TaskId { get; set; } = null!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        // var orDefault = Branches.GetOrDefault(context) ?? ["Done"];
        // var strArray = orDefault.ToArray();
        var workflowBusiness = context.GetService<IWorkflowBusiness>();
        if (workflowBusiness is null)
        {
            throw new ApplicationException("Workflow business not found");
        }

        var title = Title.Get(context);
        var userId = AssignToUserId.Get(context);
        var workflow = await workflowBusiness.AssignTaskToUser(title, userId);
        if (workflow == null)
        {
            throw new Exception("Workflow not found");
        }

        var stimulus = new CreateTaskStimulus(workflow.Id);
        var options = new CreateBookmarkArgs
        {
            Stimulus = new EventStimulus(workflow.Id.ToString()),
            IncludeActivityInstanceId = false,
            Callback = OnResumeAsync
        };
        context.CreateBookmark(options);
        Console.WriteLine($"Assigned task to user: {stimulus.TaskId}");
        TaskId.Set(context, workflow.Id.ToString());
    }
    private async ValueTask OnResumeAsync(ActivityExecutionContext context)
    {
        var input = context.GetWorkflowInput<object?>();
        context.SetResult(input);
        await context.CompleteActivityAsync();
    }
}

[FlowNode("Approved", "Rejected", "Cancelled")]
[Activity("Synergy", "Assign task to role")]
public class AssignTaskToRole : CodeActivity
{
    [Input(Description = "Assign task to role")]
    public string RoleCode { get; set; }

    protected override void Execute(ActivityExecutionContext context)
    {
    }
}

[Activity("Synergy", "Send a notification to user")]
public class Notification : CodeActivity
{
    [Input(Description = "The user to send the notification to")]
    public string UserId { get; set; }

    [Input(Description = "The message to send")]
    public string Message { get; set; }

    protected override void Execute(ActivityExecutionContext context)
    {
        Console.WriteLine("Hello world!");
    }
}