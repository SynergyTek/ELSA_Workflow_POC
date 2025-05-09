using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
public enum DataColumnTypeEnum
{
    [Description("Text")]
    Text = 1,
    [Description("Bool")]
    Bool = 2,
    [Description("DateTime")]
    DateTime = 3,
    [Description("Integer")]
    Integer = 4,
    [Description("Double")]
    Double = 5,
    [Description("Long")]
    Long = 6,
    [Description("TextArray")]
    TextArray = 7,
    [Description("Time")]
    Time = 8,
}
public enum UdfUITypeEnum
{
    textfield = 1,
    textarea = 2,
    number = 3,
    password = 4,
    checkbox = 5,
    selectboxes = 6,
    radio = 7,
    select = 8,
    datetime = 9,
    time = 10,
    file = 11,
    hidden = 12,
    signature = 13,
    day = 14,
    currency = 15,
    tags = 16,
    phoneNumber = 17,
    url = 18,
    email = 19,
    datagrid = 20,
    htmlelement = 21,
    content = 22,
    editgrid = 23,
    button = 24,

}
//public enum NtsActiveUserTypeEnum
//{
//    Requester = 1,
//    Owner = 2,
//    Assignee = 3,
//    SharedWith = 4,
//    SharedBy = 5,
//    None = 6,
//    OwnerOrRequester = 7,
//    All = 8,
//    PermittedUser = 9,
//    Custom = 10
//}
//public enum NtsViewTypeEnum : int
//{
//    Default = 1,
//    Book = 2,
//    Classic = 3,
//    Email = 4
//}
public enum LayoutModeEnum : int
{
    Main = 0,
    Iframe = 1,
    Popup = 2,
    Tab = 3,
    Card = 4,
    None = 5,
    Div = 6
}
public enum OrderByEnum : int
{
    Ascending = 1,
    Descending = 2
}
public enum DataActionEnum
{
    Create = 1,
    Edit = 2,
    Delete = 3,
    Read = 4,
    None = 5,
    View = 6
}
public enum StatusEnum
{
    // Null = 0,
    [Description("Active")]
    [Display(Name = "Active")]
    Active = 1,
    [Display(Name = "Inactive")]
    [Description("Inactive")]
    Inactive = 2
}

