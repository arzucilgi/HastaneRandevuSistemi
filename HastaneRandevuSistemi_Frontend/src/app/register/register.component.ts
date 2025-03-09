import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(
      private http: HttpClient,
      private router:Router
    ) {}

   tcKimlikNo: string = '';  
    name: string = '';  
    surname: string = '';  
    cinsiyet: string = '';  
    anneName: string = '';  
    babaName: string = '';  
    dogumYeri: string = '';  
    dogumTarihi: string = ''; 
    telefon: string = '';  
    sifre: string = '';  
    isAdmin: boolean = false; 

    uyeOl(event: Event) {
      event.preventDefault();  
  
      const uyeOlObj = {
        TcKimlikNo: this.tcKimlikNo,
        Name: this.name,
        Surname: this.surname,
        Cinsiyet: this.cinsiyet,
        AnneName: this.anneName,
        BabaName: this.babaName,
        DogumYeri: this.dogumYeri,
        DogumTarihi: this.dogumTarihi,
        Telefon: this.telefon,
        Sifre: this.sifre,
        IsAdmin: this.isAdmin,
      };
  
      this.http.post('http://localhost:5153/api/Authentication/Register', uyeOlObj).subscribe({
        next: (response) => {
          console.log('Kayıt başarılı:', response);
          alert('Başarıyla üye oldunuz!');
          this.router.navigate(['/login']);  
        },
        error: (error) => {
          console.error('Kayıt hatası:', error);
          alert('Kayıt başarısız. Lütfen bilgilerinizi kontrol edin.');
        }
      });
    }

}
