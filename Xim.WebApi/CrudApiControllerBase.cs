using AutoMapper;
using Xim;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Xim.WebApi
{  
    public abstract class CrudApiControllerBase<TEntity, TDto> : ControllerBase
        where TEntity : class
        where TDto : class 
    {
        protected readonly IGenericRepository<TEntity> _repository;

        public CrudApiControllerBase(IGenericRepository<TEntity> repository)
        {
            this._repository = repository;
        }

        [HttpGet] 
        public IEnumerable<TDto> Get([FromServices] IMapper mapper)
        {
            return mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(this._repository.GetAll());
        }

        [HttpGet("{id}")]
        public TDto Get(int id, [FromServices] IMapper mapper) => mapper.Map<TEntity, TDto>(this._repository.Get(id));

        [HttpPost]
        public TDto Post([FromBody] TDto command, [FromServices] IMapper mapper)
        {
            var entity = mapper.Map<TDto, TEntity>(command);
            return mapper.Map<TEntity, TDto>( this._repository.Save(entity));
        }

        [HttpPut("{id}")]
        public TDto Put(int id, [FromBody] TDto command, [FromServices] IMapper mapper)
        {
            var entity = mapper.Map<TDto, TEntity>(command);
            return mapper.Map<TEntity, TDto>(this._repository.Update(entity));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pessoa = this._repository.Get(id);
            if (pessoa == null)
                return BadRequest("Entity not found");

            return Ok(this._repository.Delete(pessoa));
        }
    }
}
