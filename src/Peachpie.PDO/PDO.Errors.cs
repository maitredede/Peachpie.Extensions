using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    partial class PDO
    {
        private PhpValue m_errorCode;
        private PhpValue m_errorInfo;

        private void ClearError()
        {
            this.m_errorCode = PhpValue.Null;
            this.m_errorInfo = PhpValue.Null;
        }

        private void HandleError(System.Exception ex)
        {
            throw new NotImplementedException();
        }

        public PhpValue errorCode()
        {
            return this.m_errorCode;
        }

        public PhpValue errorInfo()
        {
            return this.m_errorInfo;
        }
    }
}
