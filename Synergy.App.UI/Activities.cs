using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime.Stimuli;
using Synergy.App.Business.Interface;
using Synergy.App.Data.ViewModels;
using Workflow = Elsa.Workflows.Activities.Workflow;

namespace Synergy.App.UI;

public record CreateTaskStimulus(Guid TaskId);

[FlowNode("Approved", "Rejected", "Cancelled")]
[Activity("Synergy", "Assign task to user")]
public class AssignTaskToUser : Activity
{
    [Input(Description = "The email of the user to assign the task to")]
    public Input<string> Email { get; set; } = null!;


    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var elsaBusiness = context.GetService<IElsaBusiness>();
        if (elsaBusiness is null)
        {
            throw new ApplicationException("Workflow business not found");
        }

        var workflowMetaActivity = (Workflow)context.WorkflowExecutionContext.Activity;
        var title = workflowMetaActivity.WorkflowMetadata.Name ?? "Workflow";
        var email = Email.Get(context);
        var user = context.WorkflowInput.TryGetValue("User", out var value)
            ? (UserViewModel)value
            : throw new ApplicationException("User not found");


        var options = new CreateBookmarkArgs
        {
            IncludeActivityInstanceId = true,
            Callback = OnResumeAsync
        };
        context.CreateBookmark(options);
    }

    private async ValueTask OnResumeAsync(ActivityExecutionContext context)
    {
        context.WorkflowInput.TryGetValue("Status", out string value);
        await context.CompleteActivityWithOutcomesAsync(value);
    }
}

[FlowNode("Approved", "Rejected", "Cancelled")]
[Activity("Synergy", "Assign task to role")]
public class AssignTaskToRole : Activity
{
    [Input(Description = "Assign task to role")]
    public Input<string> RoleCode { get; set; } = null!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var role = RoleCode.Get(context);
        var user = context.WorkflowInput.TryGetValue("User", out var value)
            ? (UserViewModel)value
            : throw new ApplicationException("User not found");

        var workflowMetaActivity = (Workflow)context.WorkflowExecutionContext.Activity;
        var title = workflowMetaActivity.WorkflowMetadata.Name ?? "Workflow";
        var elsaBusiness = context.GetService<IElsaBusiness>();
        if (elsaBusiness is null)
        {
            throw new ApplicationException("Workflow business not found");
        }

        var workflow = await elsaBusiness.AssignTaskToRole(title, role, user.Id);
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
    }

    private async ValueTask OnResumeAsync(ActivityExecutionContext context)
    {
        context.WorkflowInput.TryGetValue("Status", out string value);
        await context.CompleteActivityWithOutcomesAsync(value);
    }
}

[Activity("Synergy", "Send a notification to user")]
public class ExecuteBusiness : CodeActivity
{
    [Input(Description = "Assign task to role")]
    public Input<List<string>> Names { get; set; } = null!;

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