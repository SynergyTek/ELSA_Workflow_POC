using System.Reflection;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime.Stimuli;
using Elsa.Workflows.UIHints;
using Elsa.Workflows.UIHints.Dropdown;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;
using Workflow = Elsa.Workflows.Activities.Workflow;


namespace Synergy.App.Core;

public class CustomDropDownOptionsProvider : DropDownOptionsProviderBase
{
    protected override async ValueTask<ICollection<SelectListItem>> GetItemsAsync(PropertyInfo propertyInfo,
        object? context, CancellationToken cancellationToken)
    {
        var interfaces = new List<Type>
        {
            typeof(IWorkflowBusiness),
            typeof(IElsaBusiness)
        };
        return (from type in interfaces
                let typeName = type.Name.Replace("`1", "").Replace("`2", "")
                let methods = type.GetMethods()
                from method in methods
                let name = string.Join(".", typeName, method.Name)
                let parameters = method.GetParameters()
                let parameterNames = string.Join(", ", parameters.Select(p => p.Name))
                select new SelectListItem($"{name}({parameterNames})", $"{type.Name}.{name}")
            )
            .ToList();
    }
}

public record CreateTaskStimulus(Guid TaskId);

[FlowNode("Approved", "Rejected", "Cancelled")]
[Activity("Synergy", "Assign task to user")]
public class AssignTaskToUser : Activity
{
    [Input(Description = "The email of the user to assign the task to")]
    public Input<string> Email { get; set; } = null!;

    // [Input(Description = "A list of expected outcomes to handle.",
    //     UIHint = InputUIHints.DynamicOutcomes,
    //     DefaultValue = new[] { "Approved", "Rejected", "Cancelled" })]
    // public Input<IList<string>> Branches { get; set; } = null!;


    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var email = Email.Get(context);
        context.WorkflowInput.TryGetValue("User", out User user);

        var workflowMetaActivity = (Workflow)context.WorkflowExecutionContext.Activity;
        var title = workflowMetaActivity.WorkflowMetadata.Name ?? "Workflow";
        var elsaBusiness = context.GetService<IElsaBusiness>();
        if (elsaBusiness is null)
        {
            throw new ApplicationException("Workflow business not found");
        }

        var workflow = await elsaBusiness.AssignTaskToUser(title, email, user);
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

[FlowNode("Approved", "Rejected", "Cancelled")]
[Activity("Synergy", "Assign task to role")]
public class AssignTaskToRole : Activity
{
    [Input(Description = "Assign task to role")]
    public Input<string> RoleCode { get; set; } = null!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var role = RoleCode.Get(context);
        context.WorkflowInput.TryGetValue("User", out User user);

        var workflowMetaActivity = (Workflow)context.WorkflowExecutionContext.Activity;
        var title = workflowMetaActivity.WorkflowMetadata.Name ?? "Workflow";
        var elsaBusiness = context.GetService<IElsaBusiness>();
        if (elsaBusiness is null)
        {
            throw new ApplicationException("Workflow business not found");
        }

        var workflow = await elsaBusiness.AssignTaskToRole(title, role, user);
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
    [Input(
        Description = "Name of business",
        UIHint = InputUIHints.DropDown,
        UIHandler = typeof(CustomDropDownOptionsProvider)
    )]
    public Input<string> BusinessName { get; set; } = null!;


    protected override void Execute(ActivityExecutionContext context)
    {
    }
}

[Activity("Synergy", "Send a notification to user")]
public class Notification : CodeActivity
{
    [Input(Description = "The user to send the notification to")]
    public Input<string> Email { get; set; }

    [Input(Description = "The message to send", UIHint = InputUIHints.MultiLine)]
    public Input<string> Message { get; set; }

    protected override void Execute(ActivityExecutionContext context)
    {
        Console.WriteLine("Hello world!");
    }
}

[Activity("Synergy", "Test Activity with all UI hints")]
public class AllUIHintsTest : CodeActivity
{
    [Input(UIHint = InputUIHints.SingleLine)]
    public Input<string> SingleLine { get; set; } = null!;

    [Input(UIHint = InputUIHints.MultiLine)]
    public Input<string> MultiLine { get; set; } = null!;

    [Input(UIHint = InputUIHints.Checkbox)]
    public Input<bool> Checkbox { get; set; } = null!;

    [Input(UIHint = InputUIHints.CheckList)]
    public Input<IList<string>> CheckList { get; set; } = null!;

    [Input(UIHint = InputUIHints.RadioList)]
    public Input<string> RadioList { get; set; } = null!;

    [Input(UIHint = InputUIHints.DropDown,
        Options = new[]
            { "text/plain", "text/html", "application/json", "application/xml", "application/x-www-form-urlencoded" })]
    public Input<string> DropDown { get; set; } = null!;

    [Input(UIHint = InputUIHints.MultiText)]
    public Input<IList<string>> MultiText { get; set; } = null!;

    [Input(UIHint = InputUIHints.CodeEditor)]
    public Input<string> CodeEditor { get; set; } = null!;

    [Input(UIHint = InputUIHints.ExpressionEditor)]
    public Input<string> ExpressionEditor { get; set; } = null!;

    [Input(UIHint = InputUIHints.VariablePicker)]
    public Input<string> VariablePicker { get; set; } = null!;

    [Input(UIHint = InputUIHints.TypePicker)]
    public Input<string> TypePicker { get; set; } = null!;

    [Input(UIHint = InputUIHints.WorkflowDefinitionPicker)]
    public Input<string> WorkflowDefinitionPicker { get; set; } = null!;

    [Input(UIHint = InputUIHints.OutputPicker)]
    public Input<string> OutputPicker { get; set; } = null!;

    [Input(UIHint = InputUIHints.OutcomePicker)]
    public Input<IList<string>> OutcomePicker { get; set; } = null!;

    [Input(UIHint = InputUIHints.JsonEditor)]
    public Input<string> JsonEditor { get; set; } = null!;

    [Input(UIHint = InputUIHints.DynamicOutcomes)]
    public Input<IList<string>> DynamicOutcomes { get; set; } = null!;

    protected override void Execute(ActivityExecutionContext context)
    {
        // Implementation here
    }
}