import { useEffect, useState } from "react";
import { QuestItemControl } from "./QuestItemControl.jsx"

export function QuestControl(props) {
    const [items, setItems] = useState([]);

    useEffect(() => {
        setItems(props.items)
    }, [])

    const onClickAdd = () => setItems(items.concat([{ title: "" }]));
    const onRemoveItem = (index) => setItems(items.filter((element, i) => i !== index));
    const onChangeItem = (index, value) => setItems(items.map((element, i) => index !== i ? element : { title: value, id: element.id }));

    if (items.length == 0) {
        onClickAdd();
    }

    return (
        <>
            <div>
                {items.map((item, index) => <QuestItemControl value={item} onRemove={onRemoveItem} onChange={onChangeItem} key={index} index={index} />)}
            </div>
            <button className="add-button" type="button" onClick={onClickAdd}>Add</button>
        </>
    );
}

