

if(window.location.pathname.split("/").pop()==='index.html')
{
     
     const radiobtn1= document.getElementById("genderradioM");
    const radiobtn2= document.getElementById('genderradioF');
    const radiobtn3= document.getElementById('genderradioC');
    const genderselect=document.getElementById('genderSelectForCustom');

    radiobtn1.addEventListener('change',()=>{
        genderselect.classList.add("d-none");
    });
    radiobtn2.addEventListener('change',()=>{
        genderselect.classList.add("d-none");
    });
    radiobtn3.addEventListener('change',()=>{
        genderselect.classList.remove("d-none");
    });

}

