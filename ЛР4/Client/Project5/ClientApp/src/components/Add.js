import React, { useState, useEffect } from 'react';
import { Link, Redirect } from 'react-router-dom';

function Add() {
    const [name, setName] = useState(null);
    const [description, setDescription] = useState(null);
    const [price, setPrice] = useState(null);

    const postProduct = () => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ "name": name, "description": description, "price": parseInt(price) })
        };
        fetch("http://localhost/ProductStore/products", requestOptions)
            .then(response => response.json());
    }

    return (
        <form>
            <p>Для добавления товара заполните следующие поля:</p>
            <div className="form-group">
                <label htmlFor="Name" className="col-md-2 control-label">Название:</label>
                <div className="col-md-4">
                    <input type="text" name="Name" className="form-control" onChange={(e) => setName(e.target.value)} />
                </div>
            </div>
            <div className="form-group">
                <label htmlFor="Description" className="col-md-2 control-label">Описание:</label>
                <div className="col-md-4">
                    <input type="text" name="Description" className="form-control" onChange={(e) => setDescription(e.target.value)} />
                </div>
            </div>
            <div className="form-group">
                <label className="col-md-2 control-label">Цена:</label>
                <div className="col-md-4">
                    <input type="text" name="Price" className="form-control" onChange={(e) => setPrice(e.target.value)} />
                </div>
            </div>
            <div className="form-group">
                <div className="col-md-offset-2 col-md-10">
                    <Link type="submit" onClick={postProduct} className="btn btn-default" to={{ pathname: '/' }}>Добавить</Link>
                </div>
            </div>
        </form>
    );
}

export { Add };
