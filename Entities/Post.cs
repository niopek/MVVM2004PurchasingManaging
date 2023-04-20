using System;
using System.ComponentModel.DataAnnotations;

namespace MVVM2004PurchasingManaging.Entities;

public class Post
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}
