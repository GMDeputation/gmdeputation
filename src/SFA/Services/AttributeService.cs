using SFA.Models;
using SFA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Services
{
    public interface IAttributeService
    {
        Task<List<AttributeTypeModel>> GetAll();
        Task<AttributeTypeModel> GetById(int id);
        Task<List<AttributeModel>> GetByTypeId(int typeId);
        Task<string> Add(AttributeTypeModel attribute, User loggedInUser);
        Task<string> Edit(AttributeTypeModel attribute,User loggedInUser);
        //Task<bool> Delete(int id,int userId);
        Task<QueryResult<AttributeModel>> Search(AttributeModelQuery query);
        Task<AttributeModel> GetByAttribute(int id);
        Task<string> SaveAttribute(AttributeModel attribute, User loggedInUser);
    }

    public class AttributeService : IAttributeService
    {
        private readonly SFADBContext _context = null;

        public AttributeService(SFADBContext context)
        {
            _context = context;
        }

        public async Task<List<AttributeTypeModel>> GetAll()
        {
            var attributeEntities = await _context.TblAttributeTypeNta.OrderBy(m => m.Name).ToListAsync();
            return attributeEntities.Select(m => new AttributeTypeModel
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();
        }
        public async Task<List<AttributeModel>> GetByTypeId(int typeId)
        {
            var attributeEntities = await _context.TblAttributeNta.Where(m => m.AttributeTypeId == typeId).OrderBy(m => m.AttributeName).ToListAsync();
            return attributeEntities.Select(m => new AttributeModel
            {
                Id = m.Id,
                AttributeName = m.AttributeName
            }).ToList();
        }
        public async Task<AttributeTypeModel> GetById(int id)
        {
            var attributeEntity = await _context.TblAttributeTypeNta.Include(m => m.TblAttributeNta).FirstOrDefaultAsync(m => m.Id == id);
            return attributeEntity == null ? null : new AttributeTypeModel
            {
                Id = attributeEntity.Id,
                Name = attributeEntity.Name,
                Attributes = attributeEntity.TblAttributeNta.Select(m => new AttributeModel
                {
                    Id = m.Id,
                    AttributeName = m.AttributeName,
                    AttributeTypeId = m.AttributeTypeId,
                    AttributeNotes = m.AttributeNotes
                }).ToList()
            };
        }

        public async Task<AttributeModel> GetByAttribute(int id)
        {
            var attributeEntity = await _context.TblAttributeNta.Include(m => m.AttributeType).FirstOrDefaultAsync(m => m.Id == id);
            return attributeEntity == null ? null : new AttributeModel
            {
                Id = attributeEntity.Id,
                AttributeName = attributeEntity.AttributeName,
                AttributeTypeId = attributeEntity.AttributeTypeId,
                AttributeNotes = attributeEntity.AttributeNotes
            };
        }

        public async Task<string> Add(AttributeTypeModel attribute, User loggedInUser)
        {
            var entity = await _context.TblAttributeTypeNta.FirstOrDefaultAsync(m => (m.Name == attribute.Name));
            if (entity != null)
            {

                return "Attribute Type name is already exists.Kindly change attribute name";
            }
            //TODO THis needs to be addressed. The type ID should relate to an attribute type not a random number
            var typeId = 1;
            var attributeEntity = new TblAttributeTypeNta
            {
                Name = attribute.Name,
                InsertDatetime = DateTime.Now,
                InsertUser = loggedInUser.Id.ToString()


            };

            var model = attribute.Attributes.Select(m => new TblAttributeNta
            {
                AttributeName = m.AttributeName,
                AttributeNotes = m.AttributeNotes,
                AttributeTypeId = typeId,
                InsertDatetime = DateTime.Now,
                InsertUser = loggedInUser.Id.ToString()
       
            }).ToList();

            attributeEntity.TblAttributeNta = model;

            try
            {
                _context.TblAttributeTypeNta.Add(attributeEntity);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> Edit(AttributeTypeModel attribute, User loggedInUser)
        {
            var entity = await _context.TblAttributeTypeNta.FirstOrDefaultAsync(m => (m.Name == attribute.Name) && m.Id != attribute.Id);
            if (entity != null)
            {

                return "Attribute type name is already exists.Kindly change attribute type name";

            }
            var attributeEntity = await _context.TblAttributeTypeNta.Include(m => m.TblAttributeNta).FirstOrDefaultAsync(m => m.Id == attribute.Id);
            attributeEntity.Name = attribute.Name;

            foreach (var detail in attribute.Attributes)
            {
                if (detail.Id == 0)
                {
                    var currAttribute = attributeEntity.TblAttributeNta.FirstOrDefault(m => m.Id == detail.Id);
                    var newAttribute = attribute.Attributes.FirstOrDefault(m => m.Id == detail.Id);
                    currAttribute.AttributeName = newAttribute.AttributeName;
                    currAttribute.AttributeNotes = newAttribute.AttributeNotes;
                    currAttribute.UpdateDatetime = DateTime.Now;
                    currAttribute.UpdateUser = loggedInUser.Id.ToString();
                }
                else
                {
                    var newAttribute = attribute.Attributes.FirstOrDefault(m => m.Id == detail.Id);
                    attributeEntity.TblAttributeNta.Add(new TblAttributeNta
                    {    
                        AttributeName = newAttribute.AttributeName,
                        AttributeNotes = newAttribute.AttributeNotes,
                        AttributeTypeId = attribute.Id,
                        InsertDatetime = DateTime.Now,
                        InsertUser = loggedInUser.Id.ToString()
                    });
                }

            }


            try
            {
                await _context.SaveChangesAsync();

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<bool> Delete(int id,int userId)
        //{
        //    var attributeEntity = await _context.TblAttribute.FirstOrDefaultAsync(m => m.Id == id);
        //    attributeEntity.IsDelete = true;
        //    attributeEntity.DeletedBy = userId;
        //    attributeEntity.DeletedOn = DateTime.Now;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<QueryResult<AttributeModel>> Search(AttributeModelQuery query)
        {
            try
            {
                var skip = (query.Page - 1) * query.Limit;
                var attributeQuery = _context.TblAttributeNta.Include(m => m.AttributeType).AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(query.Name))
                {
                    attributeQuery = attributeQuery.Where(m => m.AttributeName.Contains(query.Name) || m.AttributeType.Name.Contains(query.Name));
                }
                var count = await attributeQuery.CountAsync();

                switch (query.Order.ToLower())
                {
                    case "typeName":
                        attributeQuery = attributeQuery.OrderBy(m => m.AttributeType.Name);
                        break;
                    case "-typeName":
                        attributeQuery = attributeQuery.OrderByDescending(m => m.AttributeType.Name);
                        break;
                    default:
                        attributeQuery = query.Order.StartsWith("-") ? attributeQuery.OrderByDescending(m => m.AttributeName) : attributeQuery.OrderBy(m => m.AttributeName);
                        break;
                }
                var attributeEntities = await attributeQuery.Skip(skip).Take(query.Limit).ToListAsync();
                var attributes = attributeEntities.Select(m => new AttributeModel
                {
                    Id = m.Id,
                    AttributeName = m.AttributeName,
                    AttributeTypeId = m.AttributeTypeId,
                    AttributeTypeName = m.AttributeType.Name,
                    AttributeNotes = m.AttributeNotes
                }).ToList();

                return new QueryResult<AttributeModel> { Result = attributes, Count = count };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveAttribute(AttributeModel attribute, User loggedInUser)
        {
            //If we are saving a new Attribute
            if (attribute.Id == 0)
            {
                var attributeEntity = new TblAttributeNta
                {                    
                    AttributeName = attribute.AttributeName,
                    AttributeTypeId = attribute.AttributeTypeId,
                    AttributeNotes = attribute.AttributeNotes,
                    InsertDatetime = DateTime.Now,
                    InsertUser = loggedInUser.Id.ToString()
            };

                _context.TblAttributeNta.Add(attributeEntity);
            }
            //We are updating an existing Attribute
            else
            {
                var attributeEntity = await _context.TblAttributeNta.FirstOrDefaultAsync(m => m.Id == attribute.Id);

                attributeEntity.AttributeName = attribute.AttributeName;
                attributeEntity.AttributeTypeId = attribute.AttributeTypeId;
                attributeEntity.AttributeNotes = attribute.AttributeNotes;
                attributeEntity.UpdateDatetime = DateTime.Now;
                attributeEntity.UpdateUser = loggedInUser.Id.ToString();

            }

            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}