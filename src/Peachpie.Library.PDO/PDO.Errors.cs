﻿using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.Library.PDO
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

        /// <inheritDoc />
        public PhpValue errorCode()
        {
            return this.m_errorCode;
        }

        /// <inheritDoc />
        public PhpValue errorInfo()
        {
            return this.m_errorInfo;
        }
    }
}
