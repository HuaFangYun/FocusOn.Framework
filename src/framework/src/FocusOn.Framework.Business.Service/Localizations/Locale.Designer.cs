﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FocusOn.Framework.Business.Service.Localizations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Locale {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Locale() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FocusOn.Framework.Business.Service.Localizations.Locale", typeof(Locale).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 实体 &apos;{0}&apos; 没有找到.
        /// </summary>
        internal static string Message_EntityNotFound {
            get {
                return ResourceManager.GetString("Message_EntityNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 方法 ‘{0}’ 没有被实现.
        /// </summary>
        internal static string Message_NotImeplementation {
            get {
                return ResourceManager.GetString("Message_NotImeplementation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 角色 &apos;{0}&apos; 已存在.
        /// </summary>
        internal static string Message_Role_NameDuplicate {
            get {
                return ResourceManager.GetString("Message_Role_NameDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 角色 &apos;{0}&apos; 没有找到.
        /// </summary>
        internal static string Message_Role_NameNotFound {
            get {
                return ResourceManager.GetString("Message_Role_NameNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 数据库保存失败.
        /// </summary>
        internal static string Message_SaveChangesFailed {
            get {
                return ResourceManager.GetString("Message_SaveChangesFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 数据库保存返回行数是 0.
        /// </summary>
        internal static string Message_SaveChangesRowIsZero {
            get {
                return ResourceManager.GetString("Message_SaveChangesRowIsZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 数据库保存成功.
        /// </summary>
        internal static string Message_SaveChangesSuccessfully {
            get {
                return ResourceManager.GetString("Message_SaveChangesSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户 &apos;{0}&apos; 创建成功.
        /// </summary>
        internal static string Message_User_Created {
            get {
                return ResourceManager.GetString("Message_User_Created", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 邮箱 &apos;{0}&apos; 重复.
        /// </summary>
        internal static string Message_User_EmailDuplicate {
            get {
                return ResourceManager.GetString("Message_User_EmailDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 手机号 &apos;{0}&apos; 重复.
        /// </summary>
        internal static string Message_User_MobileDuplicate {
            get {
                return ResourceManager.GetString("Message_User_MobileDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 登录失败，用户名或密码错误.
        /// </summary>
        internal static string Message_User_SignInFailed {
            get {
                return ResourceManager.GetString("Message_User_SignInFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户名 &apos;{0}&apos; 重复.
        /// </summary>
        internal static string Message_User_UserNameDuplicate {
            get {
                return ResourceManager.GetString("Message_User_UserNameDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户名 &apos;{0}&apos; 已存在.
        /// </summary>
        internal static string Message_User_UserNameDuplicate1 {
            get {
                return ResourceManager.GetString("Message_User_UserNameDuplicate1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户 &apos;{0}&apos; 没有找到.
        /// </summary>
        internal static string Message_User_UserNameNotFound {
            get {
                return ResourceManager.GetString("Message_User_UserNameNotFound", resourceCulture);
            }
        }
    }
}
