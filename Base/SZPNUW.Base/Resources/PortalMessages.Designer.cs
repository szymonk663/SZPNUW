﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SZPNUW.Base.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PortalMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PortalMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SZPNUW.Base.Resources.PortalMessages", typeof(PortalMessages).Assembly);
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
        ///   Looks up a localized string similar to Nie można dodać studenta do sekcji..
        /// </summary>
        public static string CanNotAddStudentToSection {
            get {
                return ResourceManager.GetString("CanNotAddStudentToSection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie można cię wypisać. Są z toba powiązane pewne spotkania..
        /// </summary>
        public static string CanNotDeleteStudentFromSection {
            get {
                return ResourceManager.GetString("CanNotDeleteStudentFromSection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zostało utworzonych {0} sekcji..
        /// </summary>
        public static string CreatedSectionsSuccesfull {
            get {
                return ResourceManager.GetString("CreatedSectionsSuccesfull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił błąd podczas dodawania elementu do bazy..
        /// </summary>
        public static string InsertDBError {
            get {
                return ResourceManager.GetString("InsertDBError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowe dane..
        /// </summary>
        public static string InvalidData {
            get {
                return ResourceManager.GetString("InvalidData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie można usunąć wpisu dla tego przedmiotu.
        /// </summary>
        public static string LastEntryForTheItem {
            get {
                return ResourceManager.GetString("LastEntryForTheItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak wskazanego semestru.
        /// </summary>
        public static string MissingSemester {
            get {
                return ResourceManager.GetString("MissingSemester", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak przedmiotu.
        /// </summary>
        public static string MissingSubject {
            get {
                return ResourceManager.GetString("MissingSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie można znaleść tego przedmiotu na takim semestrze..
        /// </summary>
        public static string NoSubjectOnSemester {
            get {
                return ResourceManager.GetString("NoSubjectOnSemester", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brak takiego elementu.
        /// </summary>
        public static string NoSuchElement {
            get {
                return ResourceManager.GetString("NoSuchElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wpis już istnieje.
        /// </summary>
        public static string ObjectExist {
            get {
                return ResourceManager.GetString("ObjectExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;Do tej sekcji przypisani są studenci i nie można jej usunąć.&quot;.
        /// </summary>
        public static string SectionWithStudents {
            get {
                return ResourceManager.GetString("SectionWithStudents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ten semestr jest powiązany z pewnymi sekcjami i nie można go usunąć z wpisu.
        /// </summary>
        public static string SemesterDependence {
            get {
                return ResourceManager.GetString("SemesterDependence", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Przedmiot o podanej nazwie już istnieje.
        /// </summary>
        public static string SubjectExist {
            get {
                return ResourceManager.GetString("SubjectExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Temat się powtarza..
        /// </summary>
        public static string TopicIsUsed {
            get {
                return ResourceManager.GetString("TopicIsUsed", resourceCulture);
            }
        }
    }
}
