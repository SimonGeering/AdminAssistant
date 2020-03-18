# Notes

[Installing Jekyll on windows](https://jekyllrb.com/docs/installation/windows/)  

[Videos on GitHub Pages](https://www.youtube.com/channel/UCo63gWfWRfEciJ98mJLIU0Q)  

[Get started with GitHub Pages](https://24ways.org/2013/get-started-with-github-pages/)  

## Markdown build

<https://github.com/neenjaw/compile-mermaid-markdown-action>  
<https://github.com/mermaid-js/mermaid/blob/develop/docs/integrations.md>  
<http://kkpattern.github.io/2015/05/15/Embed-Chart-in-Jekyll.html>  
<https://stackoverflow.com/questions/53883747/how-to-make-github-pages-markdown-support-mermaid-diagram>  
<https://github.com/neenjaw/mermaid-markdown-test>  
<https://github.com/jekyll/minima>  

## CLI Tools

Assume starting from src folder.

### Entity framework CLI commands

EF migrations are kept in DAL to allow adding other Apps in future with a common DB.

``` dos
dotnet ef database update --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --no-build
dotnet ef migrations add Initial --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --output-dir "./EntityFramework/Migrations" --no-build
dotnet ef migrations list --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj"
dotnet ef migrations remove --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --no-build
```

### Install Global tools

``` dos
dotnet tool install dotnet-ef --global --version 3.1.0 --interactive
dotnet tool uninstall --global dotnet-ef
dotnet tool list --global
```

### Docs

``` dos
cd ..
cd docs
bundle exec jekyll serve --livereload
```

### NPM
