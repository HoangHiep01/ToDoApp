using System;
using System.Collections.Generic;

namespace toDoApp.Models;

public partial class Tag
{
    public long Id { get; set; }

    public string Tag1 { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
