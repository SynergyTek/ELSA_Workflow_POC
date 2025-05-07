namespace Synergy.App.Data;

public enum Roles
{
    // User Roles
    Admin = 1,
    User = 2,
    Guest = 3,
}

public enum NotificationType
{
    // Notification Types
    Email = 1,
    Sms = 2,
    Push = 3,
}

public enum NotificationStatus
{
    // Notification Status
    Sent = 1,
    Failed = 2,
    Pending = 3,
}

public enum WorkflowStatus
{
    // Workflow Status
    Inprogress = 1,
    Cancelled = 2,
    Inactive = 3,
    Completed = 4,
}

public enum LeaveType
{
    SickLeave = 1,
    AnnualLeave = 2,
    CasualLeave = 3,
}