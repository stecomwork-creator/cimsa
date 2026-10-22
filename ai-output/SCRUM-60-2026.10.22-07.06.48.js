const irsaliyeler=[{no:"IRS-001",durum:"Aktif"},{no:"IRS-002",durum:"Aktif"},{no:"IRS-003",durum:"Iptal"}];
function iptalEt(i){const r=irsaliyeler[i];if(!r||r.durum==="Iptal")return;if(confirm(r.no+" iptal edilsin mi?")){r.durum="Iptal";ciz()}}
function ciz(){document.body.innerHTML="<h1>Irsaliye Listesi</h1>"+irsaliyeler.map((r i)=>`<p>${r.no} - <b>${r.durum}</b> <button ${r.durum==="Iptal"?"disabled":""} onclick="iptalEt(${i})">Iptal</button></p>`).join("")}
ciz();