/* MIT License

Copyright (c) 2016 JetBrains http://www.jetbrains.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

using System;

#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming

namespace CSDeskBand.Annotations
{
  /// <summary>
  /// Indicates that the value of the marked element could be <c>null</c> sometimes,
  /// so the check for <c>null</c> is necessary before its usage.
  /// </summary>
  /// <example><code>
  /// [CanBeNull] object Test() => null;
  /// 
  /// void UseTest() {
  ///   var p = Test();
  ///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
  /// }
  /// </code></example>
  [AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
  internal sealed class CanBeNullAttribute : Attribute { }

  /// <summary>
  /// Indicates that the value of the marked element could never be <c>null</c>.
  /// </summary>
  /// <example><code>
  /// [NotNull] object Foo() {
  ///   return null; // Warning: Possible 'null' assignment
  /// }
  /// </code></example>
  [AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
  internal sealed class NotNullAttribute : Attribute { }

  /// <summary>
  /// Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
  /// and Lazy classes to indicate that the value of a collection item, of the Task.Result property
  /// or of the Lazy.Value property can never be null.
  /// </summary>
  [AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field)]
  internal sealed class ItemNotNullAttribute : Attribute { }

  /// <summary>
  /// Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
  /// and Lazy classes to indicate that the value of a collection item, of the Task.Result property
  /// or of the Lazy.Value property can be null.
  /// </summary>
  [AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field)]
  internal sealed class ItemCanBeNullAttribute : Attribute { }

  /// <summary>
  /// Indicates that the marked method builds string by format pattern and (optional) arguments.
  /// Parameter, which contains format string, should be given in constructor. The format string
  /// should be in <see cref="string.Format(IFormatProvider,string,object[])"/>-like form.
  /// </summary>
  /// <example><code>
  /// [StringFormatMethod("message")]
  /// void ShowError(string message, params object[] args) { /* do something */ }
  /// 
  /// void Foo() {
  ///   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
  /// }
  /// </code></example>
  [AttributeUsage(
    AttributeTargets.Constructor | AttributeTargets.Method |
    AttributeTargets.Property | AttributeTargets.Delegate)]
  internal sealed class StringFormatMethodAttribute : Attribute
  {
    /// <param name="formatParameterName">
    /// Specifies which parameter of an annotated method should be treated as format-string
    /// </param>
    internal StringFormatMethodAttribute([NotNull] string formatParameterName)
    {
      FormatParameterName = formatParameterName;
    }

    [NotNull] internal string FormatParameterName { get; private set; }
  }

  /// <summary>
  /// For a parameter that is expected to be one of the limited set of values.
  /// Specify fields of which type should be used as values for this parameter.
  /// </summary>
  [AttributeUsage(
    AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field,
    AllowMultiple = true)]
  internal sealed class ValueProviderAttribute : Attribute
  {
    internal ValueProviderAttribute([NotNull] string name)
    {
      Name = name;
    }

    [NotNull] internal string Name { get; private set; }
  }

  /// <summary>
  /// Indicates that the function argument should be string literal and match one
  /// of the parameters of the caller function. For example, ReSharper annotates
  /// the parameter of <see cref="System.ArgumentNullException"/>.
  /// </summary>
  /// <example><code>
  /// void Foo(string param) {
  ///   if (param == null)
  ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
  /// }
  /// </code></example>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class InvokerParameterNameAttribute : Attribute { }

  /// <summary>
  /// Indicates that the method is contained in a type that implements
  /// <c>System.ComponentModel.INotifyPropertyChanged</c> interface and this method
  /// is used to notify that some property value changed.
  /// </summary>
  /// <remarks>
  /// The method should be non-static and conform to one of the supported signatures:
  /// <list>
  /// <item><c>NotifyChanged(string)</c></item>
  /// <item><c>NotifyChanged(params string[])</c></item>
  /// <item><c>NotifyChanged{T}(Expression{Func{T}})</c></item>
  /// <item><c>NotifyChanged{T,U}(Expression{Func{T,U}})</c></item>
  /// <item><c>SetProperty{T}(ref T, T, string)</c></item>
  /// </list>
  /// </remarks>
  /// <example><code>
  /// internal class Foo : INotifyPropertyChanged {
  ///   internal event PropertyChangedEventHandler PropertyChanged;
  /// 
  ///   [NotifyPropertyChangedInvocator]
  ///   protected virtual void NotifyChanged(string propertyName) { ... }
  ///
  ///   string _name;
  /// 
  ///   internal string Name {
  ///     get { return _name; }
  ///     set { _name = value; NotifyChanged("LastName"); /* Warning */ }
  ///   }
  /// }
  /// </code>
  /// Examples of generated notifications:
  /// <list>
  /// <item><c>NotifyChanged("Property")</c></item>
  /// <item><c>NotifyChanged(() =&gt; Property)</c></item>
  /// <item><c>NotifyChanged((VM x) =&gt; x.Property)</c></item>
  /// <item><c>SetProperty(ref myField, value, "Property")</c></item>
  /// </list>
  /// </example>
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
  {
    internal NotifyPropertyChangedInvocatorAttribute() { }
    internal NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
    {
      ParameterName = parameterName;
    }

    [CanBeNull] internal string ParameterName { get; private set; }
  }

  /// <summary>
  /// Describes dependency between method input and output.
  /// </summary>
  /// <syntax>
  /// <p>Function Definition Table syntax:</p>
  /// <list>
  /// <item>FDT      ::= FDTRow [;FDTRow]*</item>
  /// <item>FDTRow   ::= Input =&gt; Output | Output &lt;= Input</item>
  /// <item>Input    ::= ParameterName: Value [, Input]*</item>
  /// <item>Output   ::= [ParameterName: Value]* {halt|stop|void|nothing|Value}</item>
  /// <item>Value    ::= true | false | null | notnull | canbenull</item>
  /// </list>
  /// If method has single input parameter, it's name could be omitted.<br/>
  /// Using <c>halt</c> (or <c>void</c>/<c>nothing</c>, which is the same) for method output
  /// means that the methos doesn't return normally (throws or terminates the process).<br/>
  /// Value <c>canbenull</c> is only applicable for output parameters.<br/>
  /// You can use multiple <c>[ContractAnnotation]</c> for each FDT row, or use single attribute
  /// with rows separated by semicolon. There is no notion of order rows, all rows are checked
  /// for applicability and applied per each program state tracked by R# analysis.<br/>
  /// </syntax>
  /// <examples><list>
  /// <item><code>
  /// [ContractAnnotation("=&gt; halt")]
  /// internal void TerminationMethod()
  /// </code></item>
  /// <item><code>
  /// [ContractAnnotation("halt &lt;= condition: false")]
  /// internal void Assert(bool condition, string text) // regular assertion method
  /// </code></item>
  /// <item><code>
  /// [ContractAnnotation("s:null =&gt; true")]
  /// internal bool IsNullOrEmpty(string s) // string.IsNullOrEmpty()
  /// </code></item>
  /// <item><code>
  /// // A method that returns null if the parameter is null,
  /// // and not null if the parameter is not null
  /// [ContractAnnotation("null =&gt; null; notnull =&gt; notnull")]
  /// internal object Transform(object data) 
  /// </code></item>
  /// <item><code>
  /// [ContractAnnotation("=&gt; true, result: notnull; =&gt; false, result: null")]
  /// internal bool TryParse(string s, out Person result)
  /// </code></item>
  /// </list></examples>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  internal sealed class ContractAnnotationAttribute : Attribute
  {
    internal ContractAnnotationAttribute([NotNull] string contract)
      : this(contract, false) { }

    internal ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
    {
      Contract = contract;
      ForceFullStates = forceFullStates;
    }

    [NotNull] internal string Contract { get; private set; }

    internal bool ForceFullStates { get; private set; }
  }

  /// <summary>
  /// Indicates that marked element should be localized or not.
  /// </summary>
  /// <example><code>
  /// [LocalizationRequiredAttribute(true)]
  /// class Foo {
  ///   string str = "my string"; // Warning: Localizable string
  /// }
  /// </code></example>
  [AttributeUsage(AttributeTargets.All)]
  internal sealed class LocalizationRequiredAttribute : Attribute
  {
    internal LocalizationRequiredAttribute() : this(true) { }

    internal LocalizationRequiredAttribute(bool required)
    {
      Required = required;
    }

    internal bool Required { get; private set; }
  }

  /// <summary>
  /// Indicates that the value of the marked type (or its derivatives)
  /// cannot be compared using '==' or '!=' operators and <c>Equals()</c>
  /// should be used instead. However, using '==' or '!=' for comparison
  /// with <c>null</c> is always permitted.
  /// </summary>
  /// <example><code>
  /// [CannotApplyEqualityOperator]
  /// class NoEquality { }
  /// 
  /// class UsesNoEquality {
  ///   void Test() {
  ///     var ca1 = new NoEquality();
  ///     var ca2 = new NoEquality();
  ///     if (ca1 != null) { // OK
  ///       bool condition = ca1 == ca2; // Warning
  ///     }
  ///   }
  /// }
  /// </code></example>
  [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct)]
  internal sealed class CannotApplyEqualityOperatorAttribute : Attribute { }

  /// <summary>
  /// When applied to a target attribute, specifies a requirement for any type marked
  /// with the target attribute to implement or inherit specific type or types.
  /// </summary>
  /// <example><code>
  /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
  /// class ComponentAttribute : Attribute { }
  /// 
  /// [Component] // ComponentAttribute requires implementing IComponent interface
  /// class MyComponent : IComponent { }
  /// </code></example>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  [BaseTypeRequired(typeof(Attribute))]
  internal sealed class BaseTypeRequiredAttribute : Attribute
  {
    internal BaseTypeRequiredAttribute([NotNull] Type baseType)
    {
      BaseType = baseType;
    }

    [NotNull] internal Type BaseType { get; private set; }
  }

  /// <summary>
  /// Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
  /// so this symbol will not be marked as unused (as well as by other usage inspections).
  /// </summary>
  [AttributeUsage(AttributeTargets.All)]
  internal sealed class UsedImplicitlyAttribute : Attribute
  {
    internal UsedImplicitlyAttribute()
      : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default) { }

    internal UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
      : this(useKindFlags, ImplicitUseTargetFlags.Default) { }

    internal UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
      : this(ImplicitUseKindFlags.Default, targetFlags) { }

    internal UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
    {
      UseKindFlags = useKindFlags;
      TargetFlags = targetFlags;
    }

    internal ImplicitUseKindFlags UseKindFlags { get; private set; }

    internal ImplicitUseTargetFlags TargetFlags { get; private set; }
  }

  /// <summary>
  /// Should be used on attributes and causes ReSharper to not mark symbols marked with such attributes
  /// as unused (as well as by other usage inspections)
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
  internal sealed class MeansImplicitUseAttribute : Attribute
  {
    internal MeansImplicitUseAttribute()
      : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default) { }

    internal MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
      : this(useKindFlags, ImplicitUseTargetFlags.Default) { }

    internal MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
      : this(ImplicitUseKindFlags.Default, targetFlags) { }

    internal MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
    {
      UseKindFlags = useKindFlags;
      TargetFlags = targetFlags;
    }

    [UsedImplicitly] internal ImplicitUseKindFlags UseKindFlags { get; private set; }

    [UsedImplicitly] internal ImplicitUseTargetFlags TargetFlags { get; private set; }
  }

  [Flags]
  internal enum ImplicitUseKindFlags
  {
    Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
    /// <summary>Only entity marked with attribute considered used.</summary>
    Access = 1,
    /// <summary>Indicates implicit assignment to a member.</summary>
    Assign = 2,
    /// <summary>
    /// Indicates implicit instantiation of a type with fixed constructor signature.
    /// That means any unused constructor parameters won't be reported as such.
    /// </summary>
    InstantiatedWithFixedConstructorSignature = 4,
    /// <summary>Indicates implicit instantiation of a type.</summary>
    InstantiatedNoFixedConstructorSignature = 8,
  }

  /// <summary>
  /// Specify what is considered used implicitly when marked
  /// with <see cref="MeansImplicitUseAttribute"/> or <see cref="UsedImplicitlyAttribute"/>.
  /// </summary>
  [Flags]
  internal enum ImplicitUseTargetFlags
  {
    Default = Itself,
    Itself = 1,
    /// <summary>Members of entity marked with attribute are considered used.</summary>
    Members = 2,
    /// <summary>Entity marked with attribute and all its members considered used.</summary>
    WithMembers = Itself | Members
  }

  /// <summary>
  /// This attribute is intended to mark internally available API
  /// which should not be removed and so is treated as used.
  /// </summary>
  [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class PublicAPIAttribute : Attribute
  {
    internal PublicAPIAttribute() { }

    internal PublicAPIAttribute([NotNull] string comment)
    {
      Comment = comment;
    }

    [CanBeNull] internal string Comment { get; private set; }
  }

  /// <summary>
  /// Tells code analysis engine if the parameter is completely handled when the invoked method is on stack.
  /// If the parameter is a delegate, indicates that delegate is executed while the method is executed.
  /// If the parameter is an enumerable, indicates that it is enumerated while the method is executed.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class InstantHandleAttribute : Attribute { }

  /// <summary>
  /// Indicates that a method does not make any observable state changes.
  /// The same as <c>System.Diagnostics.Contracts.PureAttribute</c>.
  /// </summary>
  /// <example><code>
  /// [Pure] int Multiply(int x, int y) => x * y;
  /// 
  /// void M() {
  ///   Multiply(123, 42); // Waring: Return value of pure method is not used
  /// }
  /// </code></example>
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class PureAttribute : Attribute { }

  /// <summary>
  /// Indicates that the return value of method invocation must be used.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class MustUseReturnValueAttribute : Attribute
  {
    internal MustUseReturnValueAttribute() { }

    internal MustUseReturnValueAttribute([NotNull] string justification)
    {
      Justification = justification;
    }

    [CanBeNull] internal string Justification { get; private set; }
  }

  /// <summary>
  /// Indicates the type member or parameter of some type, that should be used instead of all other ways
  /// to get the value that type. This annotation is useful when you have some "context" value evaluated
  /// and stored somewhere, meaning that all other ways to get this value must be consolidated with existing one.
  /// </summary>
  /// <example><code>
  /// class Foo {
  ///   [ProvidesContext] IBarService _barService = ...;
  /// 
  ///   void ProcessNode(INode node) {
  ///     DoSomething(node, node.GetGlobalServices().Bar);
  ///     //              ^ Warning: use value of '_barService' field
  ///   }
  /// }
  /// </code></example>
  [AttributeUsage(
    AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Method |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.GenericParameter)]
  internal sealed class ProvidesContextAttribute : Attribute { }

  /// <summary>
  /// Indicates that a parameter is a path to a file or a folder within a web project.
  /// Path can be relative or absolute, starting from web root (~).
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class PathReferenceAttribute : Attribute
  {
    internal PathReferenceAttribute() { }

    internal PathReferenceAttribute([NotNull, PathReference] string basePath)
    {
      BasePath = basePath;
    }

    [CanBeNull] internal string BasePath { get; private set; }
  }

  /// <summary>
  /// An extension method marked with this attribute is processed by ReSharper code completion
  /// as a 'Source Template'. When extension method is completed over some expression, it's source code
  /// is automatically expanded like a template at call site.
  /// </summary>
  /// <remarks>
  /// Template method body can contain valid source code and/or special comments starting with '$'.
  /// Text inside these comments is added as source code when the template is applied. Template parameters
  /// can be used either as additional method parameters or as identifiers wrapped in two '$' signs.
  /// Use the <see cref="MacroAttribute"/> attribute to specify macros for parameters.
  /// </remarks>
  /// <example>
  /// In this example, the 'forEach' method is a source template available over all values
  /// of enumerable types, producing ordinary C# 'foreach' statement and placing caret inside block:
  /// <code>
  /// [SourceTemplate]
  /// internal static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
  ///   foreach (var x in xs) {
  ///      //$ $END$
  ///   }
  /// }
  /// </code>
  /// </example>
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class SourceTemplateAttribute : Attribute { }

  /// <summary>
  /// Allows specifying a macro for a parameter of a <see cref="SourceTemplateAttribute">source template</see>.
  /// </summary>
  /// <remarks>
  /// You can apply the attribute on the whole method or on any of its additional parameters. The macro expression
  /// is defined in the <see cref="MacroAttribute.Expression"/> property. When applied on a method, the target
  /// template parameter is defined in the <see cref="MacroAttribute.Target"/> property. To apply the macro silently
  /// for the parameter, set the <see cref="MacroAttribute.Editable"/> property value = -1.
  /// </remarks>
  /// <example>
  /// Applying the attribute on a source template method:
  /// <code>
  /// [SourceTemplate, Macro(Target = "item", Expression = "suggestVariableName()")]
  /// internal static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; collection) {
  ///   foreach (var item in collection) {
  ///     //$ $END$
  ///   }
  /// }
  /// </code>
  /// Applying the attribute on a template method parameter:
  /// <code>
  /// [SourceTemplate]
  /// internal static void something(this Entity x, [Macro(Expression = "guid()", Editable = -1)] string newguid) {
  ///   /*$ var $x$Id = "$newguid$" + x.ToString();
  ///   x.DoSomething($x$Id); */
  /// }
  /// </code>
  /// </example>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = true)]
  internal sealed class MacroAttribute : Attribute
  {
    /// <summary>
    /// Allows specifying a macro that will be executed for a <see cref="SourceTemplateAttribute">source template</see>
    /// parameter when the template is expanded.
    /// </summary>
    [CanBeNull] internal string Expression { get; set; }

    /// <summary>
    /// Allows specifying which occurrence of the target parameter becomes editable when the template is deployed.
    /// </summary>
    /// <remarks>
    /// If the target parameter is used several times in the template, only one occurrence becomes editable;
    /// other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence,
    /// use values >= 0. To make the parameter non-editable when the template is expanded, use -1.
    /// </remarks>>
    internal int Editable { get; set; }

    /// <summary>
    /// Identifies the target parameter of a <see cref="SourceTemplateAttribute">source template</see> if the
    /// <see cref="MacroAttribute"/> is applied on a template method.
    /// </summary>
    [CanBeNull] internal string Target { get; set; }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
  internal sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
  {
    internal AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }

    [NotNull] internal string Format { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
  internal sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
  {
    internal AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }

    [NotNull] internal string Format { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
  internal sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
  {
    internal AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }

    [NotNull] internal string Format { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
  internal sealed class AspMvcMasterLocationFormatAttribute : Attribute
  {
    internal AspMvcMasterLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }

    [NotNull] internal string Format { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
  internal sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
  {
    internal AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }

    [NotNull] internal string Format { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
  internal sealed class AspMvcViewLocationFormatAttribute : Attribute
  {
    internal AspMvcViewLocationFormatAttribute([NotNull] string format)
    {
      Format = format;
    }

    [NotNull] internal string Format { get; private set; }
  }

  /// <summary>
  /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
  /// is an MVC action. If applied to a method, the MVC action name is calculated
  /// implicitly from the context. Use this attribute for custom wrappers similar to
  /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
  internal sealed class AspMvcActionAttribute : Attribute
  {
    internal AspMvcActionAttribute() { }

    internal AspMvcActionAttribute([NotNull] string anonymousProperty)
    {
      AnonymousProperty = anonymousProperty;
    }

    [CanBeNull] internal string AnonymousProperty { get; private set; }
  }

  /// <summary>
  /// ASP.NET MVC attribute. Indicates that a parameter is an MVC area.
  /// Use this attribute for custom wrappers similar to
  /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcAreaAttribute : Attribute
  {
    internal AspMvcAreaAttribute() { }

    internal AspMvcAreaAttribute([NotNull] string anonymousProperty)
    {
      AnonymousProperty = anonymousProperty;
    }

    [CanBeNull] internal string AnonymousProperty { get; private set; }
  }

  /// <summary>
  /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is
  /// an MVC controller. If applied to a method, the MVC controller name is calculated
  /// implicitly from the context. Use this attribute for custom wrappers similar to
  /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
  internal sealed class AspMvcControllerAttribute : Attribute
  {
    internal AspMvcControllerAttribute() { }

    internal AspMvcControllerAttribute([NotNull] string anonymousProperty)
    {
      AnonymousProperty = anonymousProperty;
    }

    [CanBeNull] internal string AnonymousProperty { get; private set; }
  }

  /// <summary>
  /// ASP.NET MVC attribute. Indicates that a parameter is an MVC Master. Use this attribute
  /// for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcMasterAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. Indicates that a parameter is an MVC model type. Use this attribute
  /// for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, Object)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcModelTypeAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
  /// partial view. If applied to a method, the MVC partial view name is calculated implicitly
  /// from the context. Use this attribute for custom wrappers similar to
  /// <c>System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
  internal sealed class AspMvcPartialViewAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  internal sealed class AspMvcSuppressViewErrorAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
  /// Use this attribute for custom wrappers similar to 
  /// <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcDisplayTemplateAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template.
  /// Use this attribute for custom wrappers similar to
  /// <c>System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcEditorTemplateAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. Indicates that a parameter is an MVC template.
  /// Use this attribute for custom wrappers similar to
  /// <c>System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcTemplateAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
  /// is an MVC view component. If applied to a method, the MVC view name is calculated implicitly
  /// from the context. Use this attribute for custom wrappers similar to
  /// <c>System.Web.Mvc.Controller.View(Object)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
  internal sealed class AspMvcViewAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
  /// is an MVC view component name.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AspMvcViewComponentAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
  /// is an MVC view component view. If applied to a method, the MVC view component view name is default.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
  internal sealed class AspMvcViewComponentViewAttribute : Attribute { }

  /// <summary>
  /// ASP.NET MVC attribute. When applied to a parameter of an attribute,
  /// indicates that this parameter is an MVC action name.
  /// </summary>
  /// <example><code>
  /// [ActionName("Foo")]
  /// internal ActionResult Login(string returnUrl) {
  ///   ViewBag.ReturnUrl = Url.Action("Foo"); // OK
  ///   return RedirectToAction("Bar"); // Error: Cannot resolve action
  /// }
  /// </code></example>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
  internal sealed class AspMvcActionSelectorAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
  internal sealed class HtmlElementAttributesAttribute : Attribute
  {
    internal HtmlElementAttributesAttribute() { }

    internal HtmlElementAttributesAttribute([NotNull] string name)
    {
      Name = name;
    }

    [CanBeNull] internal string Name { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
  internal sealed class HtmlAttributeValueAttribute : Attribute
  {
    internal HtmlAttributeValueAttribute([NotNull] string name)
    {
      Name = name;
    }

    [NotNull] internal string Name { get; private set; }
  }

  /// <summary>
  /// Razor attribute. Indicates that a parameter or a method is a Razor section.
  /// Use this attribute for custom wrappers similar to 
  /// <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
  internal sealed class RazorSectionAttribute : Attribute { }

  /// <summary>
  /// Indicates how method, constructor invocation or property access
  /// over collection type affects content of the collection.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property)]
  internal sealed class CollectionAccessAttribute : Attribute
  {
    internal CollectionAccessAttribute(CollectionAccessType collectionAccessType)
    {
      CollectionAccessType = collectionAccessType;
    }

    internal CollectionAccessType CollectionAccessType { get; private set; }
  }

  [Flags]
  internal enum CollectionAccessType
  {
    /// <summary>Method does not use or modify content of the collection.</summary>
    None = 0,
    /// <summary>Method only reads content of the collection but does not modify it.</summary>
    Read = 1,
    /// <summary>Method can change content of the collection but does not add new elements.</summary>
    ModifyExistingContent = 2,
    /// <summary>Method can add new elements to the collection.</summary>
    UpdatedContent = ModifyExistingContent | 4
  }

  /// <summary>
  /// Indicates that the marked method is assertion method, i.e. it halts control flow if
  /// one of the conditions is satisfied. To set the condition, mark one of the parameters with 
  /// <see cref="AssertionConditionAttribute"/> attribute.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class AssertionMethodAttribute : Attribute { }

  /// <summary>
  /// Indicates the condition parameter of the assertion method. The method itself should be
  /// marked by <see cref="AssertionMethodAttribute"/> attribute. The mandatory argument of
  /// the attribute is the assertion type.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class AssertionConditionAttribute : Attribute
  {
    internal AssertionConditionAttribute(AssertionConditionType conditionType)
    {
      ConditionType = conditionType;
    }

    internal AssertionConditionType ConditionType { get; private set; }
  }

  /// <summary>
  /// Specifies assertion type. If the assertion method argument satisfies the condition,
  /// then the execution continues. Otherwise, execution is assumed to be halted.
  /// </summary>
  internal enum AssertionConditionType
  {
    /// <summary>Marked parameter should be evaluated to true.</summary>
    IS_TRUE = 0,
    /// <summary>Marked parameter should be evaluated to false.</summary>
    IS_FALSE = 1,
    /// <summary>Marked parameter should be evaluated to null value.</summary>
    IS_NULL = 2,
    /// <summary>Marked parameter should be evaluated to not null value.</summary>
    IS_NOT_NULL = 3,
  }

  /// <summary>
  /// Indicates that the marked method unconditionally terminates control flow execution.
  /// For example, it could unconditionally throw exception.
  /// </summary>
  [Obsolete("Use [ContractAnnotation('=> halt')] instead")]
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class TerminatesProgramAttribute : Attribute { }

  /// <summary>
  /// Indicates that method is pure LINQ method, with postponed enumeration (like Enumerable.Select,
  /// .Where). This annotation allows inference of [InstantHandle] annotation for parameters
  /// of delegate type by analyzing LINQ method chains.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class LinqTunnelAttribute : Attribute { }

  /// <summary>
  /// Indicates that IEnumerable, passed as parameter, is not enumerated.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class NoEnumerationAttribute : Attribute { }

  /// <summary>
  /// Indicates that parameter is regular expression pattern.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class RegexPatternAttribute : Attribute { }

  /// <summary>
  /// Prevents the Member Reordering feature from tossing members of the marked class.
  /// </summary>
  /// <remarks>
  /// The attribute must be mentioned in your member reordering patterns
  /// </remarks>
  [AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum)]
  internal sealed class NoReorderAttribute : Attribute { }

  /// <summary>
  /// XAML attribute. Indicates the type that has <c>ItemsSource</c> property and should be treated
  /// as <c>ItemsControl</c>-derived type, to enable inner items <c>DataContext</c> type resolve.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  internal sealed class XamlItemsControlAttribute : Attribute { }

  /// <summary>
  /// XAML attribute. Indicates the property of some <c>BindingBase</c>-derived type, that
  /// is used to bind some item of <c>ItemsControl</c>-derived type. This annotation will
  /// enable the <c>DataContext</c> type resolve for XAML bindings for such properties.
  /// </summary>
  /// <remarks>
  /// Property should have the tree ancestor of the <c>ItemsControl</c> type or
  /// marked with the <see cref="XamlItemsControlAttribute"/> attribute.
  /// </remarks>
  [AttributeUsage(AttributeTargets.Property)]
  internal sealed class XamlItemBindingOfItemsControlAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  internal sealed class AspChildControlTypeAttribute : Attribute
  {
    internal AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
    {
      TagName = tagName;
      ControlType = controlType;
    }

    [NotNull] internal string TagName { get; private set; }

    [NotNull] internal Type ControlType { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
  internal sealed class AspDataFieldAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
  internal sealed class AspDataFieldsAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Property)]
  internal sealed class AspMethodPropertyAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  internal sealed class AspRequiredAttributeAttribute : Attribute
  {
    internal AspRequiredAttributeAttribute([NotNull] string attribute)
    {
      Attribute = attribute;
    }

    [NotNull] internal string Attribute { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Property)]
  internal sealed class AspTypePropertyAttribute : Attribute
  {
    internal bool CreateConstructorReferences { get; private set; }

    internal AspTypePropertyAttribute(bool createConstructorReferences)
    {
      CreateConstructorReferences = createConstructorReferences;
    }
  }

  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  internal sealed class RazorImportNamespaceAttribute : Attribute
  {
    internal RazorImportNamespaceAttribute([NotNull] string name)
    {
      Name = name;
    }

    [NotNull] internal string Name { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  internal sealed class RazorInjectionAttribute : Attribute
  {
    internal RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
    {
      Type = type;
      FieldName = fieldName;
    }

    [NotNull] internal string Type { get; private set; }

    [NotNull] internal string FieldName { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  internal sealed class RazorDirectiveAttribute : Attribute
  {
    internal RazorDirectiveAttribute([NotNull] string directive)
    {
      Directive = directive;
    }

    [NotNull] internal string Directive { get; private set; }
  }

  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  internal sealed class RazorPageBaseTypeAttribute : Attribute
  {
      internal RazorPageBaseTypeAttribute([NotNull] string baseType)
      {
        BaseType = baseType;
      }
      internal RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
      {
          BaseType = baseType;
          PageName = pageName;
      }

      [NotNull] internal string BaseType { get; private set; }
      [CanBeNull] internal string PageName { get; private set; }
  }
    
  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class RazorHelperCommonAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Property)]
  internal sealed class RazorLayoutAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class RazorWriteLiteralMethodAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Method)]
  internal sealed class RazorWriteMethodAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Parameter)]
  internal sealed class RazorWriteMethodParameterAttribute : Attribute { }
}