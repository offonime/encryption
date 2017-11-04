Imports System.Security.Cryptography

Public Class frmTest
    Private Sub btnNewKeys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewKeys.Click
        CreateNewKeys()
    End Sub
    Private Sub CreateNewKeys()
        Dim Keys As encryption.Keypair = encryption.Keypair.CreateNewKeys
        txtPrivateKey.Text = Keys.Privatekey
        txtPublicKey.Text = Keys.Publickey
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateNewKeys()
    End Sub
    Private Sub EncryptMessage()
        Try
            Dim EncryptedMessage As encryption.RSAResult = encryption.RSA.Encrypt(txtMessageToEncrypt.Text, txtPublicKey.Text)
            Dim DecryptedMessage As encryption.RSAResult = encryption.RSA.Decrypt(EncryptedMessage.AsBytes, txtPrivateKey.Text)
            txtEncryptedBase64.Text = EncryptedMessage.AsBase64String
            txtDecryptedMessage.Text = DecryptedMessage.AsString
            txtErrorMessage.Text = "OK"
        Catch ex As Exception
            txtErrorMessage.Text = ex.Message
        End Try
    End Sub

    Private Sub btnEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEncrypt.Click
        EncryptMessage()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OFD.ShowDialog()
        Dim B(0 To FileSystem.FileLen(OFD.FileName) - 1) As Byte
        FileSystem.FileOpen(1, OFD.FileName, OpenMode.Binary, OpenAccess.Read)
        FileSystem.FileGet(1, B)
        FileSystem.FileClose(1)

        Dim P As String = InputBox("Enter your password")
        ' Dim Ee() = Encrypt(B, P)

        FileSystem.FileOpen(1, OFD.FileName & ".enc", OpenMode.Binary, OpenAccess.Write)
        'FileSystem.FilePut(1, Ee)
        ' FileSystem.FileClose(1)
    End Sub
End Class
