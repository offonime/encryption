Imports System.Security.Cryptography
Imports encryptionMain.RSA
Public Class Form1
    Private Sub btnNewKeys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewKeys.Click
        CreateNewKeys()
    End Sub
    Private Sub CreateNewKeys()
        Dim Keys As encryptionMain.Keypair = encryptionMain.Keypair.CreateNewKeys
        txtPrivateKey.Text = Keys.Privatekey
        txtPublicKey.Text = Keys.Publickey
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateNewKeys()
    End Sub
    Private Sub EncryptMessage()
        Try
            Dim EncryptedMessage As encryptionMain.RSAResult = Encryption.RSA.Encrypt(txtMessageToEncrypt.Text, txtPublicKey.Text)
            Dim DecryptedMessage As encryptionMain.RSAResult = Encryption.RSA.Decrypt(EncryptedMessage.AsBytes, txtPrivateKey.Text)
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
End Class
