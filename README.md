# EvaluationCase

.Net 6 frameworku ve .Net Core Onion Architecture mimarisi üzerine CQRS patterni kullanılarak geliştirilmiştir. 
Bu şekilde command ve queries sorguları birbirinden ayrı handler edilmiştir.  Handler edilen sınıflar arasındaki iletişimi, 
loosely coupled olarak tek bir noktadan sağlamak adına MediatR kütüphanesi kullanılmıştır. Proje içerisindeki performansı arttırmaya yönelik
Redis Cache ve Memory Cache implementasyonları yapılmıştır. Proje içerisinde Redis Cache tercih edilmiştir (Dev ortamında monitorin işlemi daha kolay olduğu için Redis kullanıldı.
Performans olarak MemoryCache kullanmak daha best practice olacaktır).

Database olarak MongoDB tercih edilmiştir. Validation olarak FluentValidation tercih edilmiştir. İstenen testler unit test olarak yazılmıştır. 
Projeye swagger entegresi yapılmıştır.

Teknoloji Başlıkları
1.	.Net 6	
2.	.Net Core WebApi
3.	Onion Architecture
4.	CQRS
5.	MediatR
6.	Redis Cache ve Memory Cache
7.	FluentValidation
8.	MongoDB
9.	Swagger
10.	Docker
11.	NGINX
12.	Unit Test

![hb drawio (1)](https://user-images.githubusercontent.com/57442029/203716688-93a91e58-596e-48ca-850d-e82fcd2f88f8.png)
![image](https://user-images.githubusercontent.com/57442029/203683868-44d4f1c4-b3f5-4a9f-aa11-3c5f2bcc53ce.png)

DB de 3 adet Products-Campaigns-Baskets table oluşturulmuştur. Kampanya hesaplama senaryosu şu şekilde işlemektedir.
1.	Product ep sine post atılarak ürünler eklenir. Her  bir ürün guid id ile products tablosuna kaydedilir.	
2.	Campaign ep sine post atılarak kampanyalar eklenir. Her bir kampanya guid id ile campaigns tablosuna kaydedilir.
3.	Kaydedilen ürünler Basket post ep si ile ürün guid id ve quantity bilgisi ile birlikte sepete eklenir. Aynı şekilde sepet de guid id ile basket tablosuna kaydedilir.
4.	Sepete ürünler eklendikten sonra kampanya hesaplama adımına geçilir. Campaign controller da campaign-evaluate ep sine basket id si gönderilerek ilgili sepetin içerisindeki ürünlere en uygun kampanya uygulanır ve geri döndürülür.

Test payload ları EvaluationCase.Test/Payloads klasörü altında bulunabilir.
