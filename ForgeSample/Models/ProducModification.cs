using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace ForgeSample.Models
{
    public class ProductModification
    {
        [Display(Name="产品名称")]
        [Required(ErrorMessage="{0}是必须的")]
        [StringLength(10,MinimumLength=2,ErrorMessage="{0}的长度应该不小于{2}，不大于{1}")]
        public string Name{get;set;}

        [Display(Name="价格")]
        [Range(0,double.MaxValue,ErrorMessage="{0}的值必须大于{1")]
        public float Price{get;set;}

        [Display(Name = "描述")]
        [MaxLength(100, ErrorMessage = "{0}的长度不可以超过{1}")]
        public string Description { get; set; }
    }
}