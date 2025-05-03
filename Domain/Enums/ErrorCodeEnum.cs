namespace Domain.Enums
{
    public enum ErrorCodeEnum
    {
        None =0,
        AlreadyExist = 100,
        FailerUpdated = 101,
        FailerDelete = 102,
        Success=200,
        ServerError = 501,



        InternalServerError =500,
        NotFound = 404,
        BadRequest = 400,
        BusinessRuleViolation=700,

    }
}
