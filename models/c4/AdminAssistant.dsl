# See https://c4model.com/
# Ref https://github.com/structurizr/dsl/blob/master/docs/language-reference.md
workspace "Admin Assistant" {

    model {
        user = person "User" "A user of my software system."

        enterprise "Admin Assistant" {

            BlazorClient = softwareSystem "Blazor Client" "Blazor Client Web App" "WebApp" {
                group AccountsModule {
                    AccountsScreen = container "Accounts Screen" "" "" ""
                }
            }

            BlazorServer = softwareSystem "Blazor Server" "Blazor Server Web API" {

                group AccountsModule {
                    BankAccountController = container "Bank Account Controller" {
                        PutBankAccount = component "Put BankAccount" "Update an existing BankAccount." "Endpoint" ""
                        PostBankAccount = component "Post BankAccount" "Creates a new BankAccount." "Endpoint" ""
                    }
                    BankAccountInfoController = container "Bank Account Info Controller"
                    BankAccountTypeController = container "Bank Account Type Controller"
                }

                group CoreModule {

                }
            }

            DatabaseServer = softwareSystem "Database Server" "SQL Server" "DatabaseServer" {
                AdminAssistantDatabase = container "Admin Assistant Database" "" "SQL Server Database"
            }
        }

        user -> BlazorClient "Uses"
        BlazorClient -> BlazorServer "Rest/Json"
        BlazorServer -> DatabaseServer "TCP/IP"
        AccountsScreen -> BankAccountController "Rest/Json"
    }

    views {

        systemLandscape {
          include *
          autoLayout
        }

        systemContext BlazorClient {
            include *
            autoLayout
        }

        systemContext BlazorServer {
          include *
          autoLayout
        }

        container BlazorServer
          include * 
          autoLayout
        }

        component BankAccountController
          include * 
          autoLayout
        }

        systemContext DatabaseServer {
          include *
          autoLayout
        }

        container DatabaseServer
          include * 
          autoLayout
        }

        component AdminAssistantDatabase
          include * 
          autoLayout
        }

        styles {
            element "Software System" {
                background #1168bd
                color #ffffff
            }
            element "WebApp" {
                shape WebBrowser
                background #1168bd
                color #ffffff
            }
            element "DatabaseServer" {
                shape Cylinder
                background #1168bd
                color #ffffff
            }
            element "Person" {
                shape person
                background #08427b
                color #ffffff
            }
        }
    }
}
