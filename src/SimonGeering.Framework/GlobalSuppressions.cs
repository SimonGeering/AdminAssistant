// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "False  positive - fails to detect generic implementation.", Scope = "type", Target = "~T:SimonGeering.Framework.Primitives.DomainEntity`1")]
[assembly: SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "False  positive - fails to detect generic implementation.", Scope = "type", Target = "~T:SimonGeering.Framework.Primitives.ValueObject")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:SimonGeering.Framework.Primitives.DomainEntity`1.Equals(System.Object)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:SimonGeering.Framework.Primitives.DomainEntity`1.Equals(AdminAssistant.Framework.Primitives.DomainEntity{`0})~System.Boolean")]
[assembly: SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Requires unknown generic", Scope = "member", Target = "~M:SimonGeering.Framework.TypeMapping.MappingProfileBase.ApplyIMapToMappings(System.Reflection.Assembly)")]
[assembly: SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Requires unknown generic", Scope = "member", Target = "~M:SimonGeering.Framework.TypeMapping.MappingProfileBase.ApplyIMapFromMappings(System.Reflection.Assembly)")]
