import React, { useState, useEffect } from 'react';
import { Link, Redirect } from 'react-router-dom';

function Home() {
    const [products, setProduct] = useState([]);

    useEffect(() => {
        fetch("http://localhost/ProductStore/products")
            .then((response) => response.json())
            .then((result) => setProduct(result))
    }, []);

    const deleteProduct = (e) => {
        const id = e.target.elements.id.value;
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
        };
        fetch(`http://localhost/ProductStore/products/${id}`, requestOptions);
    }

    return (
        <div>
            <h3>Каталог товаров</h3>
            <table className="table table-bordered">
                <thead>
                    <tr className="table-dark">
                        <td>Название</td>
                        <td>Описание</td>
                        <td>Цена</td>
                        <td></td>
                        <td></td>
                    </tr>
                </thead>
                {products.map(product => (
                    <tbody>
                        <tr>
                            <td>{product.name}</td>
                            <td>{product.description}</td>
                            <td>{product.price}</td>
                            <tr><Link className="btn btn-default" to={{ pathname: '/Update', data: product }}>Изменить</Link></tr>
                            <td><form onSubmit={deleteProduct}>
                                    <input type="hidden" value={product.id} name="id"/>
                                    <button className="btn btn-default">Удалить</button>
                                </form>
                            </td>
                        </tr>
                    </tbody>
                ))}
            </table>
            <Link className="btn btn-default" to={{ pathname: '/Add' }}>Добавить</Link>
        </div>
    );
}

export { Home };
