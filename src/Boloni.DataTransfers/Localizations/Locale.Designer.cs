﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Boloni.DataTransfers.Localizations {
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
    public class Locale {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Locale() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Boloni.DataTransfers.Localizations.Locale", typeof(Locale).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 邮箱.
        /// </summary>
        public static string Display_User_Email {
            get {
                return ResourceManager.GetString("Display_User_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 手机号.
        /// </summary>
        public static string Display_User_Mobile {
            get {
                return ResourceManager.GetString("Display_User_Mobile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 密码.
        /// </summary>
        public static string Display_User_Password {
            get {
                return ResourceManager.GetString("Display_User_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户名.
        /// </summary>
        public static string Display_User_UserName {
            get {
                return ResourceManager.GetString("Display_User_UserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} 是必填项.
        /// </summary>
        public static string Message_FieldIsRequired {
            get {
                return ResourceManager.GetString("Message_FieldIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户名已经存在.
        /// </summary>
        public static string Message_UserNameDuplicate {
            get {
                return ResourceManager.GetString("Message_UserNameDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户名或密码错误.
        /// </summary>
        public static string Message_UserSignInFailed {
            get {
                return ResourceManager.GetString("Message_UserSignInFailed", resourceCulture);
            }
        }
    }
}