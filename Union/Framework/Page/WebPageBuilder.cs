using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Union.Framework.Attributes;
using Union.Framework.Components.Extendable;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Page
{
    public static class WebPageBuilder
    {
        public static void InitPage(IPage page)
        {
            InitComponents(page, page);
        }

        public static List<T> CreateItems<T>(IContainer container, IEnumerable<string> ids)
        {
            return ids.Select(id => CreateComponent<T>(container.ParentPage, container, id)).Cast<T>().ToList();
        }

        public static IComponent CreateComponent<T>(IContainer container, params object[] additionalArgs)
        {
            var component = CreateComponent(
                container.ParentPage,
                container,
                typeof(T),
                new WebComponentAttribute(additionalArgs));
            InitComponents(container.ParentPage, component);
            return component;
        }

        public static IComponent CreateComponent<T>(IPage page, params object[] additionalArgs)
        {
            var component = CreateComponent(page, page, typeof(T), new WebComponentAttribute(additionalArgs));
            InitComponents(page, component);
            return component;
        }

        public static IComponent CreateComponent<T>(
            IPage page,
            object componentContainer,
            params object[] additionalArgs)
        {
            var component = CreateComponent(
                page,
                componentContainer,
                typeof(T),
                new WebComponentAttribute(additionalArgs));
            InitComponents(page, component);
            return component;
        }

        public static IComponent CreateComponent(IPage page, Type type, params object[] additionalArgs)
        {
            var component = CreateComponent(page, page, type, new WebComponentAttribute(additionalArgs));
            InitComponents(page, component);
            return component;
        }

        public static IComponent CreateComponent(
            IPage page,
            object componentContainer,
            Type type,
            IComponentAttribute attribute)
        {
            var args = typeof(ItemBase).IsAssignableFrom(type)
                           ? new List<object> { componentContainer }
                           : new List<object> { page };
            if (attribute.Args != null)
            {
                var container = componentContainer as IContainer;
                if (container != null)
                {
                    for (var i = 0; i < attribute.Args.Length; i++)
                    {
                        attribute.Args[i] = CreateInnerSelector(container, attribute.Args[i]);
                    }
                }
                args.AddRange(attribute.Args);
            }
            var component = (IComponent)Activator.CreateInstance(type, args.ToArray());
            component.ComponentName = attribute.ComponentName;
            return component;
        }

        private static object CreateInnerSelector(IContainer container, object argument)
        {
            var argumentString = argument as string;
            if (argumentString != null && argumentString.StartsWith("root:"))
            {
                return container.InnerScss(argumentString.Replace("root:", string.Empty));
            }
            return argument;
        }

        public static void InitComponents(IPage page, object componentsContainer)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page", "page cannot be null");
            }
            if (componentsContainer == null)
            {
                componentsContainer = page;
            }
            var type = componentsContainer.GetType();
            var components = GetComponents(type);
            foreach (var memberInfo in components.Keys)
            {
                var attribute = components[memberInfo];
                IComponent instance;
                if (memberInfo is FieldInfo)
                {
                    var fieldInfo = memberInfo as FieldInfo;
                    instance = CreateComponent(page, componentsContainer, fieldInfo.FieldType, attribute);
                    fieldInfo.SetValue(componentsContainer, instance);
                }
                else if (memberInfo is PropertyInfo)
                {
                    var propertyInfo = memberInfo as PropertyInfo;
                    instance = CreateComponent(page, componentsContainer, propertyInfo.PropertyType, attribute);
                    propertyInfo.SetValue(componentsContainer, instance);
                }
                else
                {
                    throw new NotSupportedException("Unknown member type");
                }
                page.RegisterComponent(instance);
                InitComponents(page, instance);
            }
        }

        private static Dictionary<MemberInfo, IComponentAttribute> GetComponents(Type type)
        {
            var components = new Dictionary<MemberInfo, IComponentAttribute>();
            var members =
                type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Cast<MemberInfo>()
                    .ToList();
            members.AddRange(type.GetProperties(BindingFlags.Public | BindingFlags.Instance));
            var attributeType = typeof(IComponentAttribute);
            foreach (var field in members)
            {
                var attributes = field.GetCustomAttributes(attributeType, true);
                if (attributes.Length == 0)
                {
                    continue;
                }
                components.Add(field, attributes[0] as IComponentAttribute);
            }
            return components;
        }

        private static bool IsComponent(FieldInfo fieldInfo)
        {
            var type = typeof(IComponent);
            return type.IsAssignableFrom(fieldInfo.FieldType);
        }
    }
}