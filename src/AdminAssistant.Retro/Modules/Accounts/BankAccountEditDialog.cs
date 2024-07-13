using System.Reflection.PortableExecutable;
using AdminAssistant.Modules.AccountsModule.UI;
using Terminal.Gui;

namespace AdminAssistant.Retro.Modules.AccountsModule;


//        <Header><div id = "Accounts_BankAccountEditDialog_Title" > @this.vm.HeaderText </ div ></ Header >
//        < Content >
//            < SfSpinner @ref="this.SfSpinner" Target="#loadingTarget" Label="Loading...." Type="SpinnerType.Fabric" />
//            <div id = "loadingTarget" class="bank-account-edit-dialog">
//                <div class="cl-row1">
//                    <label>Account name</label>
//                    <div class="account-name">
//                        <SfTextBox Value = "@this.vm.AccountName" ValueChanged="@((value) => this.vm.OnAccountNameChanged(value))" Placeholder="@this.vm.AccountNameValidationMessage" CssClass="@this.vm.AccountNameValidationClass"></SfTextBox>
//                    </div>
//                    <SfCheckBox @bind-Checked="@this.vm.IsBudgeted" Label="On budget"></SfCheckBox>
//                </div>

//                <div class="cl-row2">
//                    <label>Account Type</label>
//                    <div class="cl-account-type">
//                        <SfDropDownList @bind-Value="@this.vm.BankAccountTypeID" OnChange="@(_ => this.vm.OnBankAccountTypeChanged())" TValue="int" TItem="BankAccountType" PopupHeight="230px" Placeholder="Account Type" DataSource="@this.vm.BankAccountTypes">
//                            <DropDownListFieldSettings Text = "Description" Value="BankAccountTypeID" />
//                        </SfDropDownList>
//                    </div>
//                    <label>Opened On</label>
//                    <div class="cl-opened-on">
//                        <SfDatePicker @bind-Value="@this.vm.OpenedOn" TValue="DateTime" Format="dd-MMM-yyyy" Placeholder="Opened on" />
//                    </div>
//                </div>
//                <div class="cl-row3">
//                    <label>Currency</label>
//                    <SfDropDownList @bind-Value="@this.vm.CurrencyID" OnChange="@(_ => this.vm.OnCurrencyChanged())" TValue="int" TItem="Currency" PopupHeight="230px" Placeholder="Currency" DataSource="@this.vm.Currencies">
//                        <DropDownListFieldSettings Text = "Symbol" Value="CurrencyID" />
//                    </SfDropDownList>
//                    @*<div class="cl-label">Opening balance</div>
//                        <div class="ui-inputgroup"><span class="ui-inputgroup-addon">$</span><input pInputText type="number" class="cl-opening-balance" [(ngModel)]="NewAccount.Balance"></div>*@
//                </div>
//            </div>
//        </Content>
//    </DialogTemplates>
//    <DialogAnimationSettings Effect = "@DialogEffect.Zoom" Duration=400 />
//    <DialogButtons>
//        <DialogButton OnClick = "@(_ => this.vm.Cancel.ExecuteAsync(null))" Disabled="@(!this.vm.Cancel.CanExecute(null))" Content="Discard" IconCss="fa fa-lg fa-trash-o" />
//        <DialogButton OnClick = "@(_ => this.vm.Save.ExecuteAsync(null))" Disabled="@(!this.vm.Save.CanExecute(null))" Content="Save" IconCss="fa fa-lg fa-floppy-o" IsPrimary="true" />
//    </DialogButtons>
//</SfDialog>


internal sealed class BankAccountEditDialog : DialogWindowBase<IBankAccountEditDialogViewModel>
{
	public TextField usernameText;

	public BankAccountEditDialog(IBankAccountEditDialogViewModel vm)
        : base(vm, vm.HeaderText)
	{
        // Create input components and labels
		var usernameLabel = new Label () {
			Text = "Username:"
		};

		usernameText = new TextField {
			// Position text field adjacent to the label
			X = Pos.Right (usernameLabel) + 1,

			// Fill remaining horizontal space
			Width = Dim.Fill (),
		};

		var passwordLabel = new Label () {
			Text = "Password:",
			X = Pos.Left (usernameLabel),
			Y = Pos.Bottom (usernameLabel) + 1
		};

		var passwordText = new TextField () {
			Secret = true,
			// align with the text box above
			X = Pos.Left (usernameText),
			Y = Pos.Top (passwordLabel),
			Width = Dim.Fill (),
		};

		// Create login button
		var btnLogin = new Button () {
			Text = "Login",
			Y = Pos.Bottom(passwordLabel) + 1,
			// center the login button horizontally
			X = Pos.Center (),
			IsDefault = true,
		};

		// When login button is clicked display a message popup
		btnLogin.Accept += (sender, e) =>
        {
			if (usernameText.Text == "admin" && passwordText.Text == "password") {
				MessageBox.Query ("Logging In", "Login Successful", "Ok");
				Application.RequestStop ();
			} else {
				MessageBox.ErrorQuery ("Logging In", "Incorrect username or password", "Ok");
			}
		};

		// Add the views to the Window
		Add (usernameLabel, usernameText, passwordLabel, passwordText, btnLogin);
	}
}
