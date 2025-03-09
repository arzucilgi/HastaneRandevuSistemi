import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-admin-giris-sayfasi',
  imports: [FormsModule],
  templateUrl: './admin-giris-sayfasi.component.html',
  styleUrl: './admin-giris-sayfasi.component.css'
})
export class AdminGirisSayfasiComponent {
   eklenenSehirName: string=""
   eklenenHastaneName:string=""
   eklenenHastaneSehirId:number|null=null
   eklenenPoliklinikName:string=""
   eklenenPoliklinikHastaneId:number|null=null
   eklenenDoktorName:string=""
   eklenenDoktorSurname:string=""
   eklenenDoktorPoliklinikId:number|null=null

   constructor(
      private http: HttpClient,
      private router: Router,
      private cookieService: CookieService 
   
    ) {}

  sehirEkle(){
    this.http.post('http://localhost:5153/api/Sehirler',{sehirName:this.eklenenSehirName},{ withCredentials: true }).subscribe({
      next:(response)=>{
        alert("Şehir başarıyla eklendi.")
        console.log(response)
      },
      error: (error) => {
        console.error('Şehir ekleme hatası:', error);
        alert('Şehir ekleme  sırasında bir hata oluştu.');
      }
    })
  }

  hastaneEkle(){
    this.http.post('http://localhost:5153/api/Hastane',{hastaneName:this.eklenenHastaneName, sehirId:this.eklenenHastaneSehirId},{ withCredentials: true }).subscribe({
      next:(response)=>{
        alert("Hastane başarıyla eklendi.")
        console.log(response)
        this.eklenenHastaneName=""
        this.eklenenHastaneSehirId=null
        
      },
      error:(error)=>{
        console.error('Hastane ekleme hatası:', error);
        alert('Hastane ekleme  sırasında bir hata oluştu.');
      }
    })
  }

  poliklinikEkle(){
    this.http.post('http://localhost:5153/api/Poliklinik',{poliklinikName:this.eklenenPoliklinikName, hastaneId:this.eklenenPoliklinikHastaneId},{ withCredentials: true}).subscribe({
      next:(response)=>{
        alert("Poliklinik başarıyla eklendi")
        console.log(response)
        this.eklenenPoliklinikName=""
        this.eklenenPoliklinikHastaneId=null
      }
    })
  }
  doktorkEkle(){
    this.http.post('http://localhost:5153/api/Doktor',{doktorName:this.eklenenDoktorName,doktorSurname:this.eklenenDoktorSurname, poliklinikId:this.eklenenDoktorPoliklinikId},{ withCredentials: true}).subscribe({
      next:(response)=>{
        alert("Poliklinik başarıyla eklendi")
        console.log(response)
        this.eklenenPoliklinikName=""
        this.eklenenPoliklinikHastaneId=null
      }
    })

  }
    

  cikisYap() {
    // Bu istek, kullanıcının çıkış yapmasını sağlamak için backend'e POST isteği gönderir ve çerezleri iletmek için withCredentials: true parametresini kullanır.
        this.http.post('http://localhost:5153/api/Authentication/Logout',{}, { withCredentials: true}).subscribe({
          next: (response) => {
            console.log('Çıkış başarılı', response);
            
            this.cookieService.delete('token', '/', '.localhost'); 
             
            localStorage.removeItem('userId')
            
    
            this.router.navigate(['/login']);
          },
          error: (error) => {
            console.error('Çıkış hatası:', error);
            alert('Çıkış sırasında bir hata oluştu.');
          }
        });
      }

}
