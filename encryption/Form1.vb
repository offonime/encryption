
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OFD.ShowDialog()
        Dim B(0 To FileSystem.FileLen(OFD.FileName) - 1) As Byte
        FileSystem.FileOpen(1, OFD.FileName, OpenMode.Binary, OpenAccess.Read)
        FileSystem.FileGet(1, B)
        FileSystem.FileClose(1)

        Dim P As String = InputBox("Enter your password")
        Dim Ee() = Encrypt(B, P)

        FileSystem.FileOpen(1, OFD.FileName & ".enc", OpenMode.Binary, OpenAccess.Write)
        FileSystem.FilePut(1, Ee)
        FileSystem.FileClose(1)
    End Sub

    Public Function Encrypt(ByVal B() As Byte, ByVal PSW As String) As Byte()

        ' generates a key from the password
        Dim pdb As System.Security.Cryptography.PasswordDeriveBytes = New System.Security.Cryptography.PasswordDeriveBytes(PSW, New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10})

        ' memory buffer that holds the output data
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream()

        ' this object holds the information about the AES encryption
        Dim AESE As System.Security.Cryptography.Aes = New System.Security.Cryptography.AesManaged()
        AESE.Key = pdb.GetBytes(AESE.KeySize / 8)
        AESE.IV = pdb.GetBytes(AESE.BlockSize / 8)

        ' create the encryption object and encrypt the data
        Dim cs As System.Security.Cryptography.CryptoStream = New System.Security.Cryptography.CryptoStream(ms, AESE.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
        cs.Write(B, 0, B.Length)
        cs.Close()
        Return ms.ToArray()
    End Function



    Public Function Decrypt(ByVal B() As Byte, ByVal PSW As String) As Byte()

        ' generates a key from the password
        Dim pdb As System.Security.Cryptography.PasswordDeriveBytes = New System.Security.Cryptography.PasswordDeriveBytes(PSW, New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10})

        ' this holds the encrypted 
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream()

        ' this object holds the information about the AES encryption
        Dim AESE As System.Security.Cryptography.Aes = New System.Security.Cryptography.AesManaged()
        AESE.Key = pdb.GetBytes(AESE.KeySize / 8)
        AESE.IV = pdb.GetBytes(AESE.BlockSize / 8)

        ' decrypt the data
        Dim cs As System.Security.Cryptography.CryptoStream = New System.Security.Cryptography.CryptoStream(ms, AESE.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
        cs.Write(B, 0, B.Length)
        cs.Close()
        Return ms.ToArray()
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OFD.ShowDialog()
        Dim B(0 To FileSystem.FileLen(OFD.FileName) - 1) As Byte
        FileSystem.FileOpen(1, OFD.FileName, OpenMode.Binary, OpenAccess.Read)
        FileSystem.FileGet(1, B)
        FileSystem.FileClose(1)

        Dim P As String = InputBox("Enter your password")
        Dim Ee() = Decrypt(B, P)

        FileSystem.FileOpen(1, OFD.FileName & ".dec", OpenMode.Binary, OpenAccess.Write)
        FileSystem.FilePut(1, Ee)
        FileSystem.FileClose(1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmTest.Show()
    End Sub
End Class
