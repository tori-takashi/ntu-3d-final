class CreateRooms < ActiveRecord::Migration[5.2]
  def change
    create_table :rooms do |t|
      t.references :user
      t.float :record, :null => false
      t.integer :order, :null => false
      t.integer :rate_changes, :null => false, :default => 0

      t.timestamps
    end
  end
end
