﻿namespace Wedding.DAL.Data.Entities.Abstractions;

public abstract class AuditableEntity : IAuditableEntity
{
    public string CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }
}