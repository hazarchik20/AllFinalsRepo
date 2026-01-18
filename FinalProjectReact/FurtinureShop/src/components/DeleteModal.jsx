import React, { useEffect, useState } from "react";

const DeleteModal = ({
  isOpen,
  onConfirm,
  onCancel,
  timeout = 5, 
}) => {
  const [secondsLeft, setSecondsLeft] = useState(timeout);

  useEffect(() => {
    if (!isOpen) return;

    setSecondsLeft(timeout);

    const interval = setInterval(() => {
      setSecondsLeft((prev) => prev - 1);
    }, 1000);

    const timer = setTimeout(() => {
      onCancel(); 
    }, timeout * 1000);

    return () => {
      clearInterval(interval);
      clearTimeout(timer);
    };
  }, [isOpen, timeout, onCancel]);

  if (!isOpen) return null;

  return (
    <div className="modal-backdrop" onClick={onCancel}>
      <div className="dm-modal" onClick={(e) => e.stopPropagation()}>
        <h3>Підтвердити видалення</h3>

        <p>
          <strong>Ви впевнені, що хочете видалити цей елемент</strong>?
        </p>

        <p style={{ color: "#6b7280", fontSize: 14 }}>
          Автоматичне скасування через <strong>{secondsLeft}</strong> сек.
        </p>

        <div className="dm-actions">
          <button className="dm-cancel" onClick={onCancel}>
            Скасувати
          </button>
          <button className="dm-delete" onClick={onConfirm}>
            Видалити
          </button>
        </div>
      </div>
    </div>
  );
};

export default DeleteModal;
