var car = new Object();

car.color = "white";
car.brand = "Kia";
car.model = "Rio";
    
function getCar()
{
    document.getElementById('car_color').innerHTML = car.color;
    document.getElementById('car_brand').innerHTML = car.brand;
    document.getElementById('car_model').innerHTML = car.model;
    
    document.getElementById('car').style.display = "block";
}