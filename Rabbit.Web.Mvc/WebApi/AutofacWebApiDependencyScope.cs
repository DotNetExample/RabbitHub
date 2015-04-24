using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Rabbit.Web.Mvc.WebApi
{
    internal sealed class AutofacWebApiDependencyScope : IDependencyScope
    {
        #region Field

        private readonly ILifetimeScope _lifetimeScope;

        #endregion Field

        #region Constructor

        public AutofacWebApiDependencyScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        #endregion Constructor

        #region Implementation of IDisposable

        /// <summary>
        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        /// </summary>
        public void Dispose()
        {
            if (_lifetimeScope != null)
                _lifetimeScope.Dispose();
        }

        #endregion Implementation of IDisposable

        #region Implementation of IDependencyScope

        /// <summary>
        /// �ӷ�Χ�м�������
        /// </summary>
        /// <returns>
        /// �������ķ���
        /// </returns>
        /// <param name="serviceType">Ҫ�����ķ���</param>
        public object GetService(Type serviceType)
        {
            return _lifetimeScope.ResolveOptional(serviceType);
        }

        /// <summary>
        /// �ӷ�Χ�м������񼯺ϡ�
        /// </summary>
        /// <returns>
        /// �������ķ��񼯺ϡ�
        /// </returns>
        /// <param name="serviceType">Ҫ�����ķ��񼯺ϡ�</param>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_lifetimeScope.IsRegistered(serviceType))
                return Enumerable.Empty<object>();

            var enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var instance = _lifetimeScope.Resolve(enumerableServiceType);
            return (IEnumerable<object>)instance;
        }

        #endregion Implementation of IDependencyScope
    }
}