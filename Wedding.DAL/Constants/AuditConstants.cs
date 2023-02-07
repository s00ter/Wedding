namespace Wedding.DAL.Constants;

public static class AuditConstants
{
    public static DateTime CreatedUpdatedAtDefaultValue => DateTime.UtcNow;
    public const string CreatedUpdatedByDefaultValue = "system";
}