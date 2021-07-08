# TCPServerClient
 Aplikasi chat dengan server dan client menggunakan TCP dan Multi-Threading

## Fitur Aplikasi
- *Multi Client* dengan *Multi-thread*
- Pesan dari server disebar ke semua client
- Setiap client bisa input nama
- Implementasi *Exception* untuk menampilkan error
- Server otomatis ditutup jika semua client disconnect

## Alur Kerja Aplikasi
### Server
1. Server dijalankan
2. Server menunggu client masuk (Listening)
3. Client yang ingin masuk diterima oleh server
4. Client yang baru masuk ditambahkan ke clientlist dan di masukkan ke thread baru
5. Server menerima input nama dan pesan dari tiap client
6. Server melakukan broadcast input client ke semua client lain yang berbeda
7. chat setiap client yang terkirim ke server di simpan di chat.txt
8. Client yang disconnect akan dihapus dari clientlist
9. Server ditutup



### Client
1. Client dijalankan
2. Client meminta request ke server
3. Request ke server diterima
4. client menginput nama dan pesan yang akan dikirim ke server
5. input dari client masuk ke server
6. input yang masuk akan di broadcast ke semua client selain pengirim
7. client ditutup 

### Flow Chart
![flow chat](https://user-images.githubusercontent.com/72484719/124857877-43991280-dfd7-11eb-9f7f-f1c7268e8638.png)


## Cara Install
- Clone repository atau download zip
- Buka file .sln dengan visual studio atau file .cs dengan IDE lain
- Jalankan Server kemudian client

## Hasil
![Screenshot (188)](https://user-images.githubusercontent.com/72484719/124858303-fbc6bb00-dfd7-11eb-8f66-eded462d0650.png)
