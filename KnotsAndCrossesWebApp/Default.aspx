<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KnotsAndCrossesWebApp.Default" MasterPageFile="~/Site1.master" %>

<asp:content contentplaceholderid="head" runat="server">
  <style type="text/css">
    td {
        width: 175px;
        height: 175px;
        border: 1px solid black;
    }
    .button {
        width: 175px;
        height: 175px;
        font-size: 700%;
    }
    .btnNewGame {
        font-size: medium;
        font-weight: bold;
    }
    label {
        font-size: x-large;
        font-weight: bold;
    }
    #divGameBoard {
        display: block; 
    }
  </style> 
</asp:content>

<asp:Content ID="PageBodyContent" ContentPlaceHolderID="PageBody" runat="Server">
    <div ID="divGameBoard" runat="server">
        <table>
            <tr>
                <td>
                    <asp:button ID="btnPos0" runat="server" OnClick="btnPos0_Click" CssClass="button" />
                </td>
                <td>
                    <asp:button ID="btnPos1" runat="server"  OnClick="btnPos1_Click" CssClass="button" />
                </td>
                <td>
                    <asp:button ID="btnPos2" runat="server"  OnClick="btnPos2_Click" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button ID="btnPos3" runat="server"  OnClick="btnPos3_Click" CssClass="button" />
                </td>
                <td>
                    <asp:button ID="btnPos4" runat="server"  OnClick="btnPos4_Click" CssClass="button" />
                </td>
                <td>
                    <asp:button ID="btnPos5" runat="server"  OnClick="btnPos5_Click" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button ID="btnPos6" runat="server"  OnClick="btnPos6_Click" CssClass="button" />
                </td>
                <td>
                    <asp:button ID="btnPos7" runat="server"  OnClick="btnPos7_Click" CssClass="button" />
                </td>
                <td>
                    <asp:button ID="btnPos8" runat="server"  OnClick="btnPos8_Click" CssClass="button" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnNewGame" runat="Server" Text="New Game" OnClick="btnNewGame_Click" CssClass="btnNewGame" Height="58px" Width="141px" />
    </div><br />

    <label id="lblGameStatusMsg" runat="server" CssClass="displayMessage"></label>
    <label id="lblErrorMsg" runat="server" CssClass="displayMessage"></label>
</asp:Content>