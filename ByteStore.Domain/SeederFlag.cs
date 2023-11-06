using System.ComponentModel.DataAnnotations;

namespace ByteStore.Domain;

public class SeederFlag
{
    public int Id { get; set; }

    public bool IsSeeded { get; set; }
    //  NA MIGRATION: IsSeeded = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
}