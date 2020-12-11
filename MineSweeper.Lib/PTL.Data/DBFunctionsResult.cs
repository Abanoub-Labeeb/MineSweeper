using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.Lib.PTL
{
    public class DBFunctionsResult : BaseEntity
    {
        #region MineSweeper_InputCounter SQL Fn. Result
        public int InputCount { get; set; }

        #endregion

        #region Hidden Properties
        /*
         * To follow onion design pattern we must inherit from  BaseEntity
         * but we should hide all it's properties / means saying they ain't mapped to the DB 
         * so no conflicts appear when calling DB objects
         */

        #region ID
        [NotMapped]
        public new Int64 Id { get; set; }
        #endregion

        #region AddedDate
        [NotMapped]
        public new DateTime? AddedDate { get; set; }
        #endregion

        #region ModifiedDate
        [NotMapped]
        public new DateTime? ModifiedDate { get; set; }
        #endregion

        #region IPAddress
        [NotMapped]
        public new string IPAddress { get; set; }
        #endregion
        
        #endregion
    }
}
