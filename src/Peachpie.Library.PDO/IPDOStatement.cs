using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.Library.PDO
{
    /// <summary>
    /// Interface of the PDOStatement class
    /// </summary>
    public interface IPDOStatement
    {
        /// <summary>
        /// Bind a column to a PHP variable.
        /// </summary>
        void bindColumn();
        void bindParam();
        void bindValue();
        void closeCursor();
        void columnCount();
        void debugDumpParams();
        void errorCode();
        void errorInfo();
        void execute();
        void fetch();
        void fetchAll();
        void fetchColumn();
        void fetchObject();
        void getAttribute();
        void getColumnMeta();
        void nextRowset();
        void rowCount();
        void setAttribute();
        void setFetchMode();
    }
}
