<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Modal.ascx.cs" Inherits="MidMarket.UI.Modal" %>

<div class="modal fade" id="globalModal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true" data-bs-backdrop="false">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalLabel"></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                    &#x2715;
                </button>
            </div>
            <div class="modal-body">
                <asp:Literal ID="modalMessageLiteral" runat="server"></asp:Literal>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<asp:HiddenField ID="hfShowModal" runat="server" Value="false" />
<asp:HiddenField ID="hfRedirigir" runat="server" Value="false" />

<script>
    document.addEventListener('DOMContentLoaded', function () {
        var showModal = document.getElementById('<%= hfShowModal.ClientID %>').value;
    if (showModal === "true") {
        var myModal = new bootstrap.Modal(document.getElementById('globalModal'));
        myModal.show();

        document.getElementById('<%= hfShowModal.ClientID %>').value = "false";

        var redirigir = document.getElementById('<%= hfRedirigir.ClientID %>').value;
        if (redirigir === "true") {
            setTimeout(function () {
                window.location.href = 'Default.aspx';
            }, 5000);
        }
    }
});
</script>
