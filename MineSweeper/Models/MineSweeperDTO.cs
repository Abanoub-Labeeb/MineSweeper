using MineSweeper.Lib.PTL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.Models
{
    public class MineSweeperDTO
    {

        #region ID
        [Display(Name = Messages.MINE_SWEEPER_INPUT_ID)]
        public Int64 Id { get; set; }
        #endregion

        #region Input
        [DataType(DataType.MultilineText)]
        [UIHint(Messages.MINE_SWEEPER_INPUT)]
        [Display(Name = Messages.MINE_SWEEPER_INPUT)]
        [Required]
        public string Input { get; set; }

        #endregion

        #region Output
        [DataType(DataType.MultilineText)]
        [UIHint(Messages.MINE_SWEEPER_OUTPUT)]
        [Display(Name = Messages.MINE_SWEEPER_OUTPUT)]
        public string Output { get; set; }

        #endregion

        #region RequestProcessTimes

        [Display(Name = Messages.MINE_SWEEPER_REQUESTPROCESSTIMES)]
        public int RequestProcessTimes { get; set; }

        #endregion

        #region AddedDate

        [Display(Name = Messages.MINE_SWEEPER_ADDEDDATE)]
        public DateTime? AddedDate { get; set; }
        
        #endregion
        
        #region ModifiedDate
        
        [Display(Name = Messages.MINE_SWEEPER_MODIFIEDDATE)]
        public DateTime? ModifiedDate { get; set; }
        
        #endregion
        
        #region IPAddress
        
        [Display(Name = Messages.MINE_SWEEPER_IPADRESS)]
        public string IPAddress { get; set; }
        
        #endregion
    }
}
