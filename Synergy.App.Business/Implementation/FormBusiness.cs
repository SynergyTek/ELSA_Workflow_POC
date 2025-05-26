using System.Linq.Expressions;
using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class FormBusiness(
    IQueryBase<TemplateViewModel> queryBase,
    IUserContext userContext,
    ITemplateBusiness templateBusiness,
    ITableBusiness tableBusiness,
    ICmsBusiness cmsBusiness)
    : IFormBusiness
{
    private readonly IUserContext _userContext = userContext;


    public async Task<CommandResult<List<IDictionary<string,object>>>> GetList(string templateCode)
    {
        var table = await tableBusiness.GetSingle(x => x.Template.Reference == templateCode,
            x => x.Template,
            x => x.CreatedBy,
            x => x.UpdatedBy);
        if (table == null)
        {
            return CommandResult<List<IDictionary<string,object>>>.Instance(null, false, "Template not found.");
        }

        var query = $"""select * from {table.Schema}."{table.Template.Name}" where not "IsDeleted" """;

        var result = await queryBase.GetRows(query, new{});
        return CommandResult<List<IDictionary<string,object>>>.Instance(result, true, "Data retrieved successfully.");
    }

    public async Task<CommandResult<dynamic>> GetSingleById(string templateCode, Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<CommandResult<dynamic>> GetSingle(string templateCode,
        Expression<Func<TemplateViewModel, bool>> where)
    {
        throw new NotImplementedException();
    }

    public async Task<CommandResult<dynamic>> Create(FormViewModel model)
    {
        var columns = new List<string>()
        {
            $""" "{nameof(model.Id)}" """,
            $""" "{nameof(model.CreatedBy)}Id" """,
            $""" "{nameof(model.UpdatedBy)}Id" """,
            $""" "{nameof(model.CreatedAt)}" """,
            $""" "{nameof(model.UpdatedAt)}" """,
            $""" "{nameof(model.IsDeleted)}" """
        };
        var values = new List<object>()
        {
            $"'{Guid.NewGuid()}'",
            $"'{_userContext.User.Id}'",
            $"'{_userContext.User.Id}'",
            $"'{DateTime.UtcNow:u}'",
            $"'{DateTime.UtcNow:u}'",
            false
        };
        foreach (var field in model.Data)
        {
            columns.Add($"\"{field.Key}\"");
            values.Add($"'{field.Value}'");
        }

        var prms = new
        {
            columns = string.Join(",", columns),
            values = string.Join(",", values)
        };
        var query = $"""
                     INSERT INTO form."{model.Template.Reference}" 
                     ({prms.columns}) 
                     VALUES ({prms.values})
                     """;
        await queryBase.ExecuteCommand(query, prms);
        return CommandResult<dynamic>.Instance(null, true, "Form created successfully.");
    }


    public async Task<CommandResult<dynamic>> Edit(FormViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<CommandResult<bool>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}