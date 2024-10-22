<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Toast.ascx.cs" Inherits="MidMarket.UI.Toast" %>

<div id="globalToast" class="toast-container position-fixed top-0 end-0 p-3" style="z-index: 10000;">
    <div id="toastMessage" class="toast align-items-center border-0" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
            <div class="toast-body">
                <asp:Literal ID="toastMessageLiteral" runat="server"></asp:Literal>
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    </div>
</div>


<asp:HiddenField ID="hfShowToast" runat="server" Value="false" />

<script>
    window.onload = function () {
        var showToast = document.getElementById('<%= hfShowToast.ClientID %>').value;
    if (showToast === "true") {
        var myToast = new bootstrap.Toast(document.getElementById('toastMessage'));
        myToast.show();

        setTimeout(function () {
            document.getElementById('toastMessage').classList.add('fade-out');
        }, 3000);

        document.getElementById('<%= hfShowToast.ClientID %>').value = "false";
    }
};
</script>
