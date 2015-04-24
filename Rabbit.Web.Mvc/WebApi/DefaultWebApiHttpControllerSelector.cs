using Autofac;
using Autofac.Core;
using Autofac.Features.Metadata;
using Rabbit.Kernel.Works;
using Rabbit.Web.Mvc.WebApi.Extensions;
using Rabbit.Web.Mvc.Works;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Rabbit.Web.Mvc.WebApi
{
    internal sealed class DefaultWebApiHttpControllerSelector : DefaultHttpControllerSelector
    {
        #region Field

        private readonly HttpConfiguration _configuration;

        #endregion Field

        #region Constructor

        public DefaultWebApiHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }

        #endregion Constructor

        #region Overrides of DefaultHttpControllerSelector

        /// <summary>
        /// Ϊ���� <see cref="T:System.Net.Http.HttpRequestMessage"/> ѡ�� <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor"/>��
        /// </summary>
        /// <returns>
        /// ���� <see cref="T:System.Net.Http.HttpRequestMessage"/> �� <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor"/> ʵ����
        /// </returns>
        /// <param name="request">HTTP ������Ϣ��</param>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();

            var areaName = routeData.GetAreaName();

            var controllerName = GetControllerName(request);

            //��������ģʽƥ���ʶ�𷽷�
            var serviceKey = (areaName + "/" + controllerName).ToLowerInvariant();

            var controllerContext = new HttpControllerContext(_configuration, routeData, request);

            Meta<Lazy<IHttpController>> info;
            var workContext = controllerContext.GetWorkContext();
            if (!TryResolve(workContext, serviceKey, out info))
                return null;
            var type = (Type)info.Metadata["ControllerType"];

            return
                new HttpControllerDescriptor(_configuration, controllerName, type);
        }

        #endregion Overrides of DefaultHttpControllerSelector

        #region Private Method

        private static bool TryResolve<T>(WorkContext workContext, object serviceKey, out T instance)
        {
            if (workContext != null && serviceKey != null)
            {
                var key = new KeyedService(serviceKey, typeof(T));
                object value;
                if (workContext.Resolve<ILifetimeScope>().TryResolveService(key, out value))
                {
                    instance = (T)value;
                    return true;
                }
            }

            instance = default(T);
            return false;
        }

        #endregion Private Method
    }
}