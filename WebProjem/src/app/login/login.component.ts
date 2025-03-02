import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  tcKimlik: string = '';
  password: string = '';
 

  constructor(
    private http: HttpClient,
    private router:Router
  ) {}

  // uyeGirisi(event: Event) {
  //   event.preventDefault(); // Sayfa yenilenmesini önler

  //   const uyeGirisiObj = {
  //     tcKimlik: this.tcKimlik,
  //     password: this.password,
  //   };

  //   this.http.post('http://localhost:5153/api/Authentication/Login', uyeGirisiObj, { withCredentials: true }).subscribe({
  //     next: (response:any ) => {
  //       console.log('Giriş başarılı:', response);

  //       localStorage.setItem('userId', response.userId);
        
  //       alert('Başarıyla giriş yaptınız!');
  //       this.router.navigate(['/home/randevuOlustur']); 
  //     },
  //     error: (error) => {
  //       console.error('Giriş hatası:', error);
  //       alert('Giriş başarısız. Bilgilerinizi kontrol edin.');
  //     }
  //   });
  // }

  uyeGirisi(event: Event) {
    event.preventDefault(); // Sayfa yenilenmesini önler
  
    const uyeGirisiObj = {
      tcKimlik: this.tcKimlik,
      password: this.password,
    };
  
    this.http.post('http://localhost:5153/api/Authentication/Login', uyeGirisiObj, { withCredentials: true }).subscribe({
      next: (response: any) => {
        console.log('Giriş başarılı:', response);
  
        localStorage.setItem('userId', response.userId);
        if (response.role=="Admin") {
          alert('Admin olarak giriş yaptınız!');
          this.router.navigate(['/admin']);
        } else {
          alert('Başarıyla giriş yaptınız!');
          this.router.navigate(['/home/randevuOlustur']);
        }
      },
      error: (error) => {
        console.error('Giriş hatası:', error);
        alert('Giriş başarısız. Bilgilerinizi kontrol edin.');
      }
    });
  }
  
  uyeOlPage(event:Event){
    this.router.navigate(['/register']); 
  }
  
  


}
