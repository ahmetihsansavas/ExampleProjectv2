# ExampleProject
. NET 8.0 kullanarak N katmanlı ve UnitOfWork Yapısı ile ilgili basic proje

# Klasör yapısı
-type(Entity,Domain):Projenin içerisindeki Entity lerin bulunduğu katman
-dataaccess(Repository,DAL):Projenin içerisinde bulunan DbContext,Migrations,Repository ve UnitOfWork Yapısının bulunduğu katman
-manager(Business):Projenin içerisindeki Manager katmanı
-system : projedeki api controllerin bulunduğu kısım
