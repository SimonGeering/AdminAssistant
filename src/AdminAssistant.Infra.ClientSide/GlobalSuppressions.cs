// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "False  positive - not possible due to param array.", Scope = "member", Target = "~M:AdminAssistant.Infra.Providers.ILoggingProvider.Start(System.String,System.String,System.String,System.Int32,System.Object[])")]
[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "False  positive - not possible due to param array.", Scope = "member", Target = "~M:AdminAssistant.Infra.Providers.ILoggingProvider.Finish(System.String,System.String,System.String,System.Int32,System.Object[])")]
[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "False  positive - not possible due to param array.", Scope = "member", Target = "~M:AdminAssistant.Infra.Providers.ILoggingProvider.Finish``1(``0,System.String,System.String,System.String,System.Int32,System.Object[])~``0")]
