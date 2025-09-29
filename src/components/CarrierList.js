import React, { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import { Users, Plus, Search } from 'lucide-react';
import { carrierService } from '../services/carrierService';

const CarrierList = ({ onCreateCarrier }) => {
  const [carriers, setCarriers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [page, setPage] = useState(1);

  useEffect(() => {
    loadCarriers();
  }, [page]);

  const loadCarriers = async () => {
    try {
      setLoading(true);
      const result = await carrierService.getCarriers(page, 25);
      setCarriers(result.data);
      setError('');
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div className="flex justify-center p-8"><div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary-500"></div></div>;
  if (error) return <div className="text-red-500 p-4 text-center">{error}</div>;

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="p-6"
    >
      <div className="flex justify-between items-center mb-6">
        <div className="flex items-center">
          <Users className="w-6 h-6 text-primary-500 mr-2" />
          <h2 className="text-2xl font-bold">Carriers</h2>
        </div>
        <button
          onClick={onCreateCarrier}
          className="flex items-center px-4 py-2 bg-primary-500 text-white rounded-lg hover:bg-primary-600"
        >
          <Plus className="w-4 h-4 mr-2" />
          Add Carrier
        </button>
      </div>

      <div className="grid gap-4">
        {carriers.map((carrier) => (
          <motion.div
            key={carrier.id}
            whileHover={{ scale: 1.02 }}
            className="bg-white rounded-lg shadow-md p-4 border border-gray-200"
          >
            <div className="flex justify-between items-start">
              <div>
                <h3 className="font-semibold text-lg">{carrier.name}</h3>
                <p className="text-gray-600">{carrier.email}</p>
                <div className="flex gap-2 mt-2">
                  {carrier.roles.map((role) => (
                    <span key={role} className="px-2 py-1 bg-blue-100 text-blue-800 rounded-full text-xs">
                      {role}
                    </span>
                  ))}
                </div>
              </div>
              <div className="text-right">
                <span className={`px-2 py-1 rounded-full text-xs ${carrier.isActive ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'}`}>
                  {carrier.isActive ? 'Active' : 'Inactive'}
                </span>
              </div>
            </div>
          </motion.div>
        ))}
      </div>

      {carriers.length === 0 && (
        <div className="text-center py-8 text-gray-500">
          No carriers found
        </div>
      )}
    </motion.div>
  );
};

export default CarrierList;