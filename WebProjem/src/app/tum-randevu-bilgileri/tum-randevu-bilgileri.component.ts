import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-tum-randevu-bilgileri',
  imports: [],
  templateUrl: './tum-randevu-bilgileri.component.html',
  styleUrl: './tum-randevu-bilgileri.component.css'
})


export class TumRandevuBilgileriComponent implements OnInit{
  // secilenSehir=JSON.parse(localStorage.getItem('secilenSehir')!)
  // secilenHastane=JSON.parse(localStorage.getItem('secilenHastane')!)
  // secilenPoliklinik=JSON.parse(localStorage.getItem('secilenPoliklinik')!)
  // secilenDoktor=JSON.parse(localStorage.getItem('secilenDoktor')!)



  girisYapanKullaniciId: string = localStorage.getItem('userId') || '';  
  randevular: any[]=[];  
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.http.get<any[]>(`http://localhost:5153/api/Randevu/user/${this.girisYapanKullaniciId}`, { withCredentials: true }).subscribe({
      next: (data) => {
        console.log(data);  
        this.randevular = data;
        console.log(this.randevular)
      },
      error: (err) => {
        console.error('Veri çekme hatası:', err); 
      }
    });
  }

  randevuSil(randevuId: number) {
    if (confirm('Bu randevuyu silmek istediğinize emin misiniz?')) {
      this.http.delete(`http://localhost:5153/api/Randevu/${randevuId}`, { withCredentials: true }).subscribe({
        next: () => {
          alert('Randevu başarıyla silindi.');
          this.randevular = this.randevular.filter(r => r.id !== randevuId);
        },
        error: (err) => {
          console.error('Randevu silme hatası:', err);
          alert('Randevu silinirken bir hata oluştu.');
        }
      });
    }
  }

}
