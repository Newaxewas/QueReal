import { useEffect, useState } from "react";
import { QuestEditItemControl } from "./QuestEditItemControl.jsx"

export function QuestEditItemListControl(props) {
    const [items, setItems] = useState([]);

    useEffect(() => setItems(props.items), [])

    const onClickAdd = () => setItems(items.concat([{ title: "" }]));
    const onRemoveItem = (index) => setItems(items.filter((element, i) => i !== index));
    const onChangeItem = (index, value) => setItems(items.map((element, i) => index !== i ? element : Object.assign({}, element, {title: value})));

    if (items.length == 0) {
        onClickAdd();
    }

    return (
        <>
            <div>
                {
                    items.map((item, index) =>
                        <QuestEditItemControl
                            value={item}
                            onRemove={onRemoveItem}
                            onChange={onChangeItem}
                            key={index}
                            index={index}
                            editMode={props.editMode} />)
                }
            </div>
            <button className="add-button" type="button" onClick={onClickAdd}>Add</button>
        </>
    );
}

