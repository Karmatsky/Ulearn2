using System;
using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        private Type type = typeof(T);

        public string GetApiDescription()
        {
            return type.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public string GetApiMethodDescription(string methodName)
        {
            var meth = GetMethod(methodName);
            return meth?.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public string[] GetApiMethodParamNames(string methodName)
        {
            var meth = GetMethod(methodName);
            return meth.GetParameters().Select(parameter => parameter.Name).ToArray();
        }

        public string[] GetApiMethodNames()
        {
            return type.GetMethods().Where(meth =>
            meth.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
            .Select(meth => meth.Name)
            .ToArray();
        }

        public ApiParamDescription GetParamDescription(ParameterInfo parameter, ApiParamDescription paramDescription)
        {
            var descrip = parameter.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault();
            if (descrip != null)
            {
                paramDescription.ParamDescription.Description = descrip.Description;
            }
            var intValidationAttribute = parameter.GetCustomAttributes<ApiIntValidationAttribute>().FirstOrDefault();
            if (intValidationAttribute != null)
            {
                paramDescription.MinValue = intValidationAttribute.MinValue;
                paramDescription.MaxValue = intValidationAttribute.MaxValue;
            }
            var reqAttribute = parameter.GetCustomAttributes<ApiRequiredAttribute>().FirstOrDefault();
            if (reqAttribute != null)
            {
                paramDescription.Required = reqAttribute.Required;
            }
            return paramDescription;
        }

        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            var meth = GetMethod(methodName);
            var parameter = meth?.GetParameters().FirstOrDefault(param => param.Name == paramName);
            return parameter?.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var paramDescrip = new ApiParamDescription();
            paramDescrip.ParamDescription = new CommonDescription(paramName);
            var meth = GetMethod(methodName);
            if (meth?.GetCustomAttribute<ApiMethodAttribute>() == null)
                return paramDescrip;
            var parameter = meth.GetParameters().Where(param => param.Name == paramName);
            if (!parameter.Any())
                return paramDescrip;
            return GetParamDescription(parameter.First(), paramDescrip);
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var fullDescrip = new ApiMethodDescription();
            var meth = GetMethod(methodName);
            if (meth?.GetCustomAttribute<ApiMethodAttribute>() == null)
            {
                return null;
            }

            fullDescrip.MethodDescription = new CommonDescription(methodName);
            fullDescrip.ParamDescriptions = meth.GetParameters()
            .Select(param => GetApiMethodParamFullDescription(methodName, param.Name))
            .ToArray();

            fullDescrip.MethodDescription.Description =
            meth.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;

            var parameter = meth.ReturnParameter;

            if (parameter.GetCustomAttributes<ApiIntValidationAttribute>().FirstOrDefault() ==
            null && parameter.GetCustomAttributes<ApiRequiredAttribute>().FirstOrDefault() == null)
            {
                return fullDescrip;
            }
            var paramDescrip = new ApiParamDescription();
            paramDescrip.ParamDescription = new CommonDescription();
            fullDescrip.ReturnDescription = GetParamDescription(parameter, paramDescrip);
            return fullDescrip;
        }

        public MethodInfo GetMethod(string methodName)
        {
            return type.GetMethod(methodName);
        }
    }
}