class CreateUsers < ActiveRecord::Migration[5.2]
  def change
    create_table :users do |t|
      t.string :user_name, :null => false
      t.string :password_digest, :null =>false
      t.integer :current_rating, :null => false, :default => 1000

      t.timestamps
    end
  end
end
