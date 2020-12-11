using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MineSweeper.Lib.PTL
{
    public class BaseEntity
    {
        #region ID
        [Column("ID")]
        [Display(Name = Messages.MINE_SWEEPER_INPUT_ID)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        #endregion
        #region AddedDate
        public DateTime? AddedDate { get; set; }
        #endregion
        #region ModifiedDate
        public DateTime? ModifiedDate { get; set; }
        #endregion
        #region IPAddress
        public string IPAddress { get; set; }
        #endregion
    }
}
