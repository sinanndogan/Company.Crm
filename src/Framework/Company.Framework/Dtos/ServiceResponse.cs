using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Company.Framework.Dtos;

public abstract class BaseResponse
{
    public bool IsSuccess { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
}

public class ErrorModel
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Field { get; }

    public string Message { get; }

    public ErrorModel(string field, string message)
    {
        Field = field;
        Message = message;
    }
}

public class ServiceResponse<TKey> : BaseResponse
{
    public ServiceResponse(bool isSuccess, TKey? data, string? message)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
    }

    public ServiceResponse(string? message) : this(true, default, message)
    { }

    public ServiceResponse(TKey data) : this(true, data, default)
    { }

    public ServiceResponse(ModelStateDictionary modelState, string message)
    {
        Message = message;
        Errors = modelState.Keys.SelectMany(key =>
            modelState[key].Errors.Select(x => new ErrorModel(key, x.ErrorMessage))
        ).ToList();
    }

    public ServiceResponse(List<ErrorModel> errors, string message)
    {
        Message = message;
        Errors = errors;
    }

    public ServiceResponse()
    { }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TKey? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ErrorModel>? Errors { get; set; }
}