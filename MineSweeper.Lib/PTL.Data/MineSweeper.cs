using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MineSweeper.Lib.PTL
{
    [Table("MineSweeper")]
    public class MineSweeper : BaseEntity
    {
        #region Input
        
        [Display(Name = Messages.MINE_SWEEPER_INPUT)]
        [Required]
        public string Input { get; set; }

        #endregion

        #region RequestProcessTimes

        [Display(Name = Messages.MINE_SWEEPER_REQUESTPROCESSTIMES)]
        public int RequestProcessTimes { get; set; }

        #endregion
    }
}
